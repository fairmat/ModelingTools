/* Copyright (C) 2012 Fairmat SRL (info@fairmat.com, http://www.fairmat.com/)
 * Author(s): Stefano Angeleri (stefano.angeleri@fairmat.com)
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DVPLDOM;
using DVPLI;

namespace PFunction2D
{
    [Serializable]
    public class PFunction2D : Function, ISerializable, IEditable,
                               IFunctionDefinition, IExportableContainer
    {
        /// <summary>
        /// The inner container for the data of the PFunction2D, providing
        /// also the actual implementation of the point search and interpolation
        /// logic.
        /// </summary>
        private CPointFunction2D function;

        public PFunction2D(Project context)
            : base(EModelParameterType.POINT_FUNCTION, context)
        {
            this.function = new CPointFunction2D();
            function.fillsomedata();
            Console.WriteLine(function.Evaluate(50, 100));
            Console.WriteLine(function.Evaluate(55, 105));
        }

        static void Main(string[] args)
        {
            CPointFunction2D a = new CPointFunction2D();
            a.fillsomedata();
            Console.WriteLine(a.Evaluate(50, 100));
            Console.WriteLine(a.Evaluate(55, 105));
        }

        public List<IExportable> ExportObjects(bool recursive)
        {
            List<IExportable> l = new List<IExportable>();
            l.Add(this);
            return l;
        }

        public List<string> Signatures
        {
            get
            {
                List<string> s = new List<string>();
                s.Add("(xy1)");
                return s;
            }
        }

        /// <summary>
        /// Gets the vector of the x cordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any cordinate.
        /// </remarks>
        internal Vector XCordinates
        {
            get
            {
                return this.function.PositionsX;
            }
        }

        /// <summary>
        /// Gets the vector of the y cordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any cordinate.
        /// </remarks>
        internal Vector YCordinates
        {
            get
            {
                return this.function.PositionsY;
            }
        }

        internal double this[int x, int y]
        {
            get
            {
                return this.function[x, y];
            }

            set
            {
                this.function[x, y] = value;
            }
        }

        /// <summary>
        /// Gets a specific value determined by the x, y index (not the actual cordinate value).
        /// </summary>
        /// <remarks>
        /// This is similar to a direct access to the underlying matrix. and the x and y values
        /// are strictly tied with the position in the Y/XCordinates.
        /// </remarks>
        /// <param name="x">The x index in the matrix of data.</param>
        /// <param name="y">The y index in the matrix of data.</param>
        /// <returns>The value at the specified indices.</returns>
        internal double GetPointValue(int x, int y)
        {
            return this.function.GetPointValue(x, y);
        }

        internal void SetPointValue(int x, int y, double value)
        {
            this.function.SetPointValue(x, y, value);
        }

        internal void SetPointValue(int x, int y, RightValue value)
        {
            this.function.SetPointValue(x, y, value);
        }

        internal void SetSizes(int x, int y)
        {
            this.function.SetSizes(x, y);
        }

        internal void CopyTo(PFunction2D other)
        {
            this.function.CopyTo(other.function);
        }

        /// <summary>
        /// Evaluates the 2D Function on the specified cordinates.
        /// </summary>
        /// <param name="x">The x cordinate to evaluate the function on.</param>
        /// <param name="y">The y cordinate to evaluate the function on.</param>
        /// <returns>
        /// The value of the function at the requested cordinates,
        /// interpolated if required.
        /// </returns>
        public double Evaluate(double x, double y)
        {
            return this.function.Evaluate(x, y);
        }

        /// <summary>
        /// Evaluates the 2D Function on the specified x cordinates. This function
        /// assumes the y cordinate is always 0 (essentially calculating the function
        /// only on the first row).
        /// </summary>
        /// <param name="x">The x cordinate to evaluate the function on.</param>
        /// <returns>
        /// The value of the function at the requested cordinate,
        /// interpolated if required.
        /// </returns>
        public override double Evaluate(double x)
        {
            return Evaluate(x, 0);
        }

        /// <summary>
        /// Evaluates the 2D Function on the specified cordinates inside a Vector.
        /// </summary>
        /// <remarks>
        /// The function will handle several cases:
        /// * Empty parameters vector
        ///   The return will always be zero.
        /// * One element in the parameters vector:
        ///   The function will be evaluated as y = f(x, 0), where x is the passed value.
        /// * Two or more elements in the parameters vector:
        ///   The first element will be used as x, the second as y, the rest will be ignored.
        ///  </remarks>
        /// <param name="parameters">
        /// The vector with the cordinates to evaluate the function on.
        /// </param>
        /// <returns>
        /// The value of the function at the requested cordinates,
        /// interpolated if required.
        /// </returns>
        public override double Evaluate(Vector parameters)
        {
            // Check the amount of passed parameters.
            if (parameters.Count == 1)
            {
                // There is a single parameter so it's managed like
                // if it was at cordinate x, 0.
                return Evaluate(parameters[0]);
            }
            else if (parameters.Count > 1)
            {
                // There are enough parameter so the first two are used.
                return Evaluate(parameters[0], parameters[1]);
            }

            // There were no parameters so zero is returned.
            return 0;
        }

        /// <summary>
        /// Handles the symbol creation and registration on the system,
        /// additionally it setups a callback for the Engine to use in
        /// order to evaluate this object.
        /// </summary>
        /// <param name="context">The project where this object is being binded to.</param>
        public override void CreateSymbol(Project context)
        {
            base.CreateSymbol(context);
            Engine.Parser.DefineCallbackFunction(VarName, new TAEDelegateFunction2D(Evaluate));
        }
    }
}
