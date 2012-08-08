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
using DVPLUtils;

namespace PFunction2D
{
    /// <summary>
    /// Represents a 2D functions defined by points and
    /// which interpolates the missing points.
    /// </summary>
    [Serializable]
    public class PFunction2D : Function, ISerializable, IEditable,
                               IFunctionDefinition, IExportableContainer
    {
        #region Serialized Variables

        /// <summary>
        /// A series of IRightValue representing the x coordinates
        /// which have been defined for the function, they should
        /// be ordered from the lowest to the highest.
        /// </summary>
        private IRightValue[] cordinatesX;

        /// <summary>
        /// A series of IRightValue representing the y coordinates
        /// which have been defined for the function, they should
        /// be ordered from the lowest to the highest.
        /// </summary>
        private IRightValue[] cordinatesY;

        /// <summary>
        /// A bi-dimensional array containing the values associated
        /// to all the coordinates  represented in <see cref="cordinatesX"/>
        /// and <see cref="cordinatesY"/>.
        /// </summary>
        private IRightValue[,] values;

        /// <summary>
        /// Defines the interpolation technique to apply, if needed.
        /// </summary>
        private EInterpolationType interpolationType;

        /// <summary>
        /// Defines the extrapolation technique to apply, if needed.
        /// </summary>
        private ExtrapolationType extrapolationType;

        /// <summary>
        /// Keeps the value of coefficient for the least squares interpolation.
        /// This is currently unused as the interpolation is not implemented.
        /// </summary>
        private int leastSquaresCoefficients;

        #endregion Serialized Variables

        #region Temporary Variables

        /// <summary>
        /// The inner container for the parsed data of the PFunction2D,
        /// providing also the actual implementation of the point
        /// search and interpolation logic.
        /// </summary>
        private CPointFunction2D function;

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Basic constructor to make a new object of type, in a similar
        /// means to other functions.
        /// </summary>
        /// <param name="context">The project this function is created in, if available.</param>
        public PFunction2D(Project context)
            : base(EModelParameterType.POINT_FUNCTION, context)
        {
            // Set some default values.
            this.interpolationType = EInterpolationType.LINEAR;
            this.extrapolationType = ExtrapolationType.CONSTANT;
            this.leastSquaresCoefficients = 0;

            // We make a function with coordinates 0 1 and all values to 0 by default.
            FillWithDefaultData();
        }

        /// <summary>
        /// Initializes the object based on the serialized data.
        /// </summary>
        /// <param name="info">The SerializationInfo that holds the serialized object data.</param>
        /// <param name="context">The StreamingContext that contains contextual
        /// information about the source.</param>
        public PFunction2D(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.cordinatesX = (IRightValue[])ObjectSerialization.GetValue2(info, "_CordinatesX", typeof(IRightValue[]));
            this.cordinatesY = (IRightValue[])ObjectSerialization.GetValue2(info, "_CordinatesY", typeof(IRightValue[]));
            this.values = (IRightValue[,])ObjectSerialization.GetValue2(info,"_Values", typeof(IRightValue[,]));
            this.interpolationType = (EInterpolationType)ObjectSerialization.GetValue2(info, "_InterpolationType", typeof(EInterpolationType));
            this.extrapolationType = (ExtrapolationType)ObjectSerialization.GetValue2(info, "_ExtrapolationType", typeof(ExtrapolationType));
            this.leastSquaresCoefficients = (int)ObjectSerialization.GetValue2(info, "_LeastSquaresCoefficients", typeof(int));
        }

        #endregion Constructors

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

        #region Properties

        /// <summary>
        /// Gets the vector of the x coordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any coordinate.
        /// </remarks>
        internal IRightValue[] XCordinates
        {
            get
            {
                return this.cordinatesX;
            }
        }

        /// <summary>
        /// Gets the vector of the y coordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any coordinate.
        /// </remarks>
        internal IRightValue[] YCordinates
        {
            get
            {
                return this.cordinatesY;
            }
        }

        /// <summary>
        /// Gets or sets the interpolation to use in case the value request
        /// is not already available.
        /// </summary>
        /// <remarks>
        /// To be used only internally, in order to set the wanted interpolation and
        /// get what is the current one.
        /// </remarks>
        internal EInterpolationType Interpolation
        {
            get
            {
                return this.interpolationType;
            }

            set
            {
                // Check for not implemented functionalities.
                if (value == EInterpolationType.SPLINE ||
                    value == EInterpolationType.LEAST_SQUARES)
                {
                    throw new Exception("The selected interpolation type is not supported.");
                }

                this.interpolationType = value;
            }
        }

        /// <summary>
        /// Gets or sets the extrapolation to use in case the value request
        /// is outside the function definition boundaries.
        /// </summary>
        /// <remarks>
        /// To be used only internally, in order to set the wanted extrapolation and
        /// get what the current one.
        /// </remarks>
        internal ExtrapolationType Extrapolation
        {
            get
            {
                return this.extrapolationType;
            }

            set
            {
                // Check for not implemented functionalities.
                if (value != ExtrapolationType.CONSTANT)
                {
                    throw new Exception("The selected extrapolation type is not supported.");
                }

                this.extrapolationType = value;
            }
        }

        /// <summary>
        /// Gets or sets the leastSquaresCoefficients to use in certain interpolation methods.
        /// is not already available.
        /// </summary>
        /// <remarks>
        /// To be used only internally, in order to set the wated
        /// leastSquaresCoefficients and get what the current one.
        /// </remarks>
        internal int LeastSquaresCoefficients
        {
            get
            {
                return this.leastSquaresCoefficients;
            }

            set
            {
                this.leastSquaresCoefficients = value;
            }
        }

        /// <summary>
        /// Gets or sets RightValue elements as coordinates for the function
        /// or as values for the function.
        /// </summary>
        /// <remarks>If both parameters are -1 nothing will be done.</remarks>
        /// <param name="x">
        /// The x coordinate to use to get or set the element,
        /// if it's -1 it will work on the y coordinates, else
        /// on the values.
        /// </param>
        /// <param name="y">
        /// The y coordinate to use to get or set the element,
        /// if it's -1 it will work on the x coordinates, else
        /// on the values.
        /// </param>
        /// <returns>The requested RightValue at the position.</returns>
        internal IRightValue this[int x, int y]
        {
            get
            {
                if (y > -1 && x > -1)
                {
                    return this.values[x, y];
                }
                else if (y != -1)
                {
                    return this.cordinatesY[y];
                }
                else if (x != -1)
                {
                    return this.cordinatesX[x];
                }

                return (RightValue)0;
            }

            set
            {
                if (y > -1 && x > -1)
                {
                    this.values[x, y] = value;
                }
                else if (y != -1)
                {
                    this.cordinatesY[y] = value;
                }
                else if (x != -1)
                {
                    this.cordinatesX[x] = value;
                }
            }
        }

        #endregion Properties

        #region Private and Internal methods

        /// <summary>
        /// Initializes the serialization information of the current object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The StreamingContext that contains contextual information
        /// about the destination.</param>
        /// <summary>
        /// Fills the function with default data, to be presented to the user when created.
        /// </summary>
        private void FillWithDefaultData()
        {
            // We make a 2x2 field.
            SetSizes(2, 2);

            // With cordinates 0 and 1 for x and y.
            this.cordinatesX[0] = (RightValue)0;
            this.cordinatesX[1] = (RightValue)1;

            this.cordinatesY[0] = (RightValue)0;
            this.cordinatesY[1] = (RightValue)1;

            // And all the values set to zero.
            this.values[0, 0] = (RightValue)0;
            this.values[0, 1] = (RightValue)0;
            this.values[1, 0] = (RightValue)0;
            this.values[1, 1] = (RightValue)0;
        }

        /// <summary>
        /// Initializes the arrays containing the cordinates
        /// and the values with arrays of the desidered size.
        /// </summary>
        /// <remarks>Anything stored is lost.</remarks>
        /// <param name="x">The size in the x dimension.</param>
        /// <param name="y">The size in the y dimension.</param>
        internal void SetSizes(int x, int y)
        {
            cordinatesX = new RightValue[x];
            cordinatesY = new RightValue[y];
            values = new RightValue[x, y];
        }

        /// <summary>
        /// Copies the data stored in this object in another object.
        /// </summary>
        /// <remarks>
        /// Anything stored in the other object is overwritten.
        /// </remarks>
        /// <param name="other">The object where to copy this object to.</param>
        internal void CopyTo(PFunction2D other)
        {
            other.SetSizes(this.cordinatesX.Length, this.cordinatesY.Length);
            other.cordinatesX = (IRightValue[])this.cordinatesX.Clone();
            other.cordinatesY = (IRightValue[])this.cordinatesY.Clone();
            other.values = (IRightValue[,])this.values.Clone();
            other.extrapolationType = this.extrapolationType;
            other.interpolationType = this.interpolationType;
        }

        #endregion Private and Internal methods

        #region Public methods

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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_CordinatesX", this.cordinatesX);
            info.AddValue("_CordinatesY", this.cordinatesY);
            info.AddValue("_Values", this.values);
            info.AddValue("_InterpolationType", this.interpolationType);
            info.AddValue("_ExtrapolationType", this.extrapolationType);
            info.AddValue("_LeastSquaresCoefficients", this.leastSquaresCoefficients);
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

        /// <summary>
        /// Parses the object.
        /// </summary>
        /// <param name="context">The project in which to parse the object.</param>
        /// <returns>True if an error occurred during the parsing, False otherwise.</returns>
        public override bool Parse(IProject context)
        {
            try
            {
                function = new CPointFunction2D(cordinatesX, cordinatesY, values,
                                                interpolationType, extrapolationType);
            }
            catch (Exception e)
            {
                context.AddError(e.Message + " for the 2D Function " + base.Name);
                return false;
            }

            return base.Parse(context);
        }

        #endregion Public methods

    }
}
