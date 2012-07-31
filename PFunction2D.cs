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
        internal double PointValue(int x, int y)
        {
            return this.function.PointValue(x, y);
        }

        /// <summary>
        /// Evaluates the 2D Function on the specified cordinates.
        /// </summary>
        /// <param name="x">The x cordinate to evaluate the function on.</param>
        /// <param name="x">The y cordinate to evaluate the function on.</param>
        /// <returns>
        /// The value of the function at the requested cordinates,
        /// interpolated if required.</returns>
        public double Evaluate(double x, double y)
        {
            return this.function.Evaluate(x, y);
        }
    }
}
