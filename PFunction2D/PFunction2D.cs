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
        private IRightValue[] coordinatesX;

        /// <summary>
        /// A series of IRightValue representing the y coordinates
        /// which have been defined for the function, they should
        /// be ordered from the lowest to the highest.
        /// </summary>
        private IRightValue[] coordinatesY;

        /// <summary>
        /// A bi-dimensional array containing the values associated
        /// to all the coordinates  represented in <see cref="coordinatesX"/>
        /// and <see cref="coordinatesY"/>.
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
            this.leastSquaresCoefficients = 2;

            // We make a function with coordinates 0 1 and all values to 0 by default.
            FillWithDefaultData();
        }

        /// <summary>
        /// Default constructor, which initializes the function with default data.
        /// </summary>
        public PFunction2D()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor to initialize the function with the specified data.
        /// </summary>
        /// <param name="coordinatesX">
        /// A <see cref="Vector"/> of coordinates, which must go from the
        /// lowest to the highest and represents the x parameter of the function.
        /// </param>
        /// <param name="coordinatesY">
        /// A <see cref="Vector"/> of coordinates, which must go from the lowest
        /// to the highest and represents the y parameter of the function.
        /// </param>
        /// <param name="values">
        /// A <see cref="Matrix"/> containing the defined data points for
        /// all the coordinates specified by coordinatesX and coordinatesY.
        /// </param>
        public PFunction2D(Vector coordinatesX, Vector coordinatesY, Matrix values)
            : this(null)
        {
            SetSizes(coordinatesX.Count, coordinatesY.Count);
            this.coordinatesX = Array.ConvertAll((double[])coordinatesX.ToArray(), element => RightValue.ConvertFrom(element, true));
            this.coordinatesY = Array.ConvertAll((double[])coordinatesY.ToArray(), element => RightValue.ConvertFrom(element, true));

            for (int x = 0; x < values.R; x++)
            {
                for (int y = 0; y < values.C; y++)
                {
                    this.values[x, y] = RightValue.ConvertFrom(values[x, y], true);
                }
            }
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
            this.coordinatesX = (IRightValue[])ObjectSerialization.GetValue2(info, "_coordinatesX", typeof(IRightValue[]));
            this.coordinatesY = (IRightValue[])ObjectSerialization.GetValue2(info, "_coordinatesY", typeof(IRightValue[]));
            this.values = (IRightValue[,])ObjectSerialization.GetValue2(info, "_Values", typeof(IRightValue[,]));
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

        #region Properties

        /// <summary>
        /// Gets the list of the signatures supported by this object,
        /// through the formula parser, to be shown to the user.
        /// </summary>
        public List<string> Signatures
        {
            get
            {
                List<string> s = new List<string>();
                s.Add("(x; y)");
                return s;
            }
        }

        /// <summary>
        /// Gets or sets the expression that generates the object.
        /// </summary>
        /// <remarks>
        /// The data is in this format:
        /// 0  X1  X2  X3
        /// Y1 Z11 Z12 Z13
        /// Y2 Z21 Z22 Z23
        /// Y3 Z31 Z32 Z33
        /// The element [0, 0] is always ignored.
        /// </remarks>
        public override Array Expr
        {
            get
            {
                // Initialize a multidimensional array able to store the data points
                // and the coordinates values.
                object[,] data = new object[this.coordinatesX.Length + 1,
                                            this.coordinatesY.Length + 1];

                // Set the first row with the data from coordinatesX
                for (int i = 0; i < this.coordinatesX.Length; i++)
                {
                    // This will set the first row starting from the second
                    // column, as the first one is unused.
                    data[i + 1, 0] = this.coordinatesX[i];
                }

                // Set the first column with the data from coordinatesY
                for (int i = 0; i < this.coordinatesY.Length; i++)
                {
                    // This will set the first column starting from the second
                    // row, as the first one is unused.
                    data[0, i + 1] = this.coordinatesY[i];
                }

                // Finally set the values of the data points.
                for (int x = 0; x < this.coordinatesX.Length; x++)
                {
                    for (int y = 0; y < this.coordinatesY.Length; y++)
                    {
                        // As the first row and column are used by the coordinates
                        // the data points will fill from the second row and second column.
                        data[x + 1, y + 1] = this.values[x, y];
                    }
                }

                return data;
            }

            set
            {
                try
                {
                    // Handles conversion from an object to an IRightValue.
                    Func<object, IRightValue> convertData = (object obj) =>
                    {
                        IRightValue result = obj as IRightValue;

                        // If the simple conversion failed it means it's something else.
                        if (result == null)
                        {
                            // Try converting the obj to a RightValue in some other means.
                            result = RightValue.ConvertFrom(obj, true);
                        }

                        return result;
                    };

                    // First of all get the sizes of the matrix and of the
                    // vectors which will have to store the data and resize
                    // the data fields in order to be able to fill them.
                    SetSizes(value.GetLength(0) - 1, value.GetLength(1) - 1);

                    // Then start converting the x coordinates in the storage array.
                    for (int i = 0; i < value.GetLength(0) - 1; i++)
                    {
                        // This will get the first row, starting
                        // from the second column as the first is one is unused.
                        this.coordinatesX[i] = convertData(value.GetValue(i + 1, 0));
                    }

                    // Then start converting the y coordinates in the storage array.
                    for (int i = 0; i < value.GetLength(1) - 1; i++)
                    {
                        // This will get the first column, starting
                        // from the second row as the first is one is unused.
                        this.coordinatesY[i] = convertData(value.GetValue(0, i + 1));
                    }

                    // Finally set the values of the data points.
                    for (int x = 0; x < this.coordinatesX.Length; x++)
                    {
                        for (int y = 0; y < this.coordinatesY.Length; y++)
                        {
                            // As the first row and column are used by the coordinates
                            // the data points will fill from the second row and second column.
                            this.values[x, y] = convertData(value.GetValue(x + 1, y + 1));
                        }
                    }
                }
                catch
                {
                    // If conversion fails, recreate with default data.
                    (new PFunction2D()).CopyTo(this);
                }
            }
        }

        /// <summary>
        /// Gets the vector of the x coordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any coordinate.
        /// </remarks>
        internal IRightValue[] Xcoordinates
        {
            get
            {
                return this.coordinatesX;
            }
        }

        /// <summary>
        /// Gets the vector of the y coordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any coordinate.
        /// </remarks>
        internal IRightValue[] Ycoordinates
        {
            get
            {
                return this.coordinatesY;
            }
        }

        /// <summary>
        /// Gets or sets the interpolation to use in case the value request
        /// is not already available.
        /// </summary>
        public EInterpolationType Interpolation
        {
            get
            {
                return this.interpolationType;
            }

            set
            {
                // Check for not implemented functionalities.
                if (value == EInterpolationType.SPLINE)
                {
                    throw new Exception("The selected interpolation type is not supported.");
                }

                this.interpolationType = value;

                // Refresh the underlying function if already present.
                if (this.function != null)
                {
                    this.function.Interpolation = value;
                }
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
        public ExtrapolationType Extrapolation
        {
            get
            {
                return this.extrapolationType;
            }

            set
            {
                this.extrapolationType = value;

                // Refresh the underlying function if already present.
                if (this.function != null)
                {
                    this.function.Extrapolation = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the leastSquaresCoefficients to use in certain interpolation methods.
        /// is not already available.
        /// </summary>
        /// <remarks>
        /// To be used only internally, in order to set the wanted
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
                    return this.coordinatesY[y];
                }
                else if (x != -1)
                {
                    return this.coordinatesX[x];
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
                    this.coordinatesY[y] = value;
                }
                else if (x != -1)
                {
                    this.coordinatesX[x] = value;
                }
            }
        }

        #endregion Properties

        #region Private and Internal methods

        /// <summary>
        /// Fills the function with default data, to be presented to the user when created.
        /// </summary>
        private void FillWithDefaultData()
        {
            // We make a 2x2 field.
            SetSizes(2, 2);

            // With coordinates 0 and 1 for x and y.
            this.coordinatesX[0] = (RightValue)0;
            this.coordinatesX[1] = (RightValue)1;

            this.coordinatesY[0] = (RightValue)0;
            this.coordinatesY[1] = (RightValue)1;

            // And all the values set to zero.
            this.values[0, 0] = (RightValue)0;
            this.values[0, 1] = (RightValue)0;
            this.values[1, 0] = (RightValue)0;
            this.values[1, 1] = (RightValue)0;
        }

        /// <summary>
        /// Initializes the arrays containing the coordinates
        /// and the values with arrays of the wanted size.
        /// </summary>
        /// <remarks>Anything stored is lost.</remarks>
        /// <param name="x">The size in the x dimension.</param>
        /// <param name="y">The size in the y dimension.</param>
        internal void SetSizes(int x, int y)
        {
            this.coordinatesX = new RightValue[x];
            this.coordinatesY = new RightValue[y];
            this.values = new RightValue[x, y];
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
            other.SetSizes(this.coordinatesX.Length, this.coordinatesY.Length);
            other.coordinatesX = (IRightValue[])this.coordinatesX.Clone();
            other.coordinatesY = (IRightValue[])this.coordinatesY.Clone();
            other.values = (IRightValue[,])this.values.Clone();
            other.extrapolationType = this.extrapolationType;
            other.interpolationType = this.interpolationType;
            other.leastSquaresCoefficients = this.leastSquaresCoefficients;
        }

        #endregion Private and Internal methods

        #region Public methods

        /// <summary>
        /// Evaluates the 2D Function on the specified coordinates.
        /// </summary>
        /// <remarks>
        /// Before calling this function always call Parse
        /// if the PFunction2D object is new or its data has been changed.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// If this function was called before this object was ever Parsed.
        /// </exception>
        /// <param name="x">The x coordinate to evaluate the function on.</param>
        /// <param name="y">The y coordinate to evaluate the function on.</param>
        /// <returns>
        /// The value of the function at the requested coordinates,
        /// interpolated if required.
        /// </returns>
        public override double Evaluate(double x, double y)
        {
            if (this.function == null)
            {
                throw new InvalidOperationException("Call Parse before calling Evaluate.");
            }

            return this.function.Evaluate(x, y);
        }

        /// <summary>
        /// Overrides the Evaluate with a single parameter of function and
        /// always throws an InvalidOperationException.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Always thrown.
        /// </exception>
        /// <param name="x">The single parameter.</param>
        /// <returns>The function doesn't return.</returns>
        public override double Evaluate(double x)
        {
            throw new InvalidOperationException("The function requires two parameters");
        }

        /// <summary>
        /// Evaluates the 2D Function on the specified coordinates inside a Vector.
        /// </summary>
        /// <remarks>
        /// The function will handle several cases:
        /// * Less or more than two elements in the parameters vector:
        ///   An exception will be thrown.
        /// * Two or more elements in the parameters vector:
        ///   The first element will be used as x and the second as y.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  If the number of the provided elements is wrong.
        /// </exception>
        /// <param name="x">
        /// The vector with the coordinates to evaluate the function on.
        /// </param>
        /// <returns>
        /// The value of the function at the requested coordinates,
        /// interpolated if required.
        /// </returns>
        public override double Evaluate(Vector x)
        {
            if (x.Count == 2)
            {
                // There are two parameters, so use them.
                return Evaluate(x[0], x[1]);
            }

            // In this case there weren't enough parameters or there were too many
            // so an Exception is thrown.
            throw new ArgumentException("Wrong amount of parameters");
        }

        /// <summary>
        /// Initializes the serialization information of the current object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The StreamingContext that contains contextual information
        /// about the destination.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("_coordinatesX", this.coordinatesX);
            info.AddValue("_coordinatesY", this.coordinatesY);
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
            Engine.Parser.DefineCallbackFunction(base.VarName, new TAEDelegateFunction2D(Evaluate));
        }

        /// <summary>
        /// Parses the object and checks if all the restraints of the 2D Point Function
        /// have been respected, if not will return an error to the parser.
        /// </summary>
        /// <remarks>
        /// Note that this method caches the object status, so any changes done to its data
        /// will require this method to be called again in order to update the cache.
        /// </remarks>
        /// <param name="context">The project in which to parse the object.</param>
        /// <returns>True if an error occurred during the parsing, False otherwise.</returns>
        public override bool Parse(IProject context)
        {
            try
            {
                this.function = new CPointFunction2D(this.coordinatesX, this.coordinatesY,
                                                     this.values, this.interpolationType,
                                                     this.extrapolationType);
                CreateSymbol(context as Project);
            }
            catch (Exception e)
            {
                context.AddError(e.Message + " for the 2D Function " + base.Name);

                // Return true as an error was raised during the parsing.
                return true;
            }

            // Return false as no error have raised during the parsing.
            return false;
        }

        /// <summary>
        /// Gets the kind of data represented by this object.
        /// </summary>
        /// <returns>The kind of data represented.</returns>
        public override string GetStringKind()
        {
            return "2D Interpolated Function";
        }

        #endregion Public methods
    }
}
