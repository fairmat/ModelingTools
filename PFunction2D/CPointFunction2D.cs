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
using DVPLI;
using DVPLUtils;

namespace PFunction2D
{
    /// <summary>
    /// Implements the evaluation functionalities of PFunction2D.
    /// It provides the capabilities to represent a matrix and give to it
    /// coordinates, additionally it allows to interpolate values between the values
    /// actually present in the matrix.
    /// </summary>
    public class CPointFunction2D
    {
        #region Function Parameters

        /// <summary>
        /// The matrix containing the value of the 2d function.
        /// </summary>
        private Matrix values;

        /// <summary>
        /// The x coordinates for the matrix.
        /// </summary>
        private Vector coordinatesX;

        /// <summary>
        /// The y coordinates for the matrix.
        /// </summary>
        private Vector coordinatesY;

        /// <summary>
        /// Defines the interpolation technique to apply, if needed.
        /// </summary>
        private EInterpolationType interpolationType;

        /// <summary>
        /// Defines the extrapolation technique to apply, if needed.
        /// </summary>
        private ExtrapolationType extrapolationType;

        #endregion Function Parameters

        #region Constructors

        /// <summary>
        /// Default constructor, just initializes the data storage.
        /// </summary>
        private CPointFunction2D()
        {
            this.values = new Matrix();
            this.coordinatesX = new Vector();
            this.coordinatesY = new Vector();
            this.interpolationType = EInterpolationType.LINEAR;
            this.extrapolationType = ExtrapolationType.CONSTANT;
        }

        /// <summary>
        /// Constructs a new CPointFunction2D through evaluation of IRightValues
        /// in order to fill the data used to evaluate the function.
        /// </summary>
        /// <param name="coordinatesX">
        /// An array of <see cref="IRightValue"/> whose result is ordered from lower to greater and will
        /// represent the x parameter of the function.
        /// </param>
        /// <param name="coordinatesY">
        /// An array of <see cref="IRightValue"/> whose result is ordered from lower to greater and will
        /// represent the y parameter of the function.
        /// </param>
        /// <param name="values">
        /// A bi-dimensional array containing the defined data points for
        /// all the coordinates specified by coordinatesX and coordinatesY.
        /// </param>
        /// <param name="interpolationType">
        /// The interpolation to apply when evaluating the function
        /// in case the requested coordinates aren't represented, but inside them.
        /// </param>
        /// <param name="extrapolationType">
        /// The extrapolation to apply when evaluating the function,
        /// in case the requested coordinates are outside the represented ones.
        /// </param>
        public CPointFunction2D(IRightValue[] coordinatesX,
                                IRightValue[] coordinatesY,
                                IRightValue[,] values,
                                EInterpolationType interpolationType,
                                ExtrapolationType extrapolationType) : this()
        {
            this.interpolationType = interpolationType;
            this.extrapolationType = extrapolationType;

            // Sets the sizes depending on the passed arrays length.
            SetSizes(coordinatesX.Length, coordinatesY.Length);

            // First copy the parsed elements for the x coordinates.
            for (int i = coordinatesX.Length - 1; i >= 0; i--)
            {
                this[i, -1] = coordinatesX[i].V();
            }

            // Then copy the parsed elements for the y coordinates.
            for (int i = coordinatesY.Length - 1; i >= 0; i--)
            {
                this[-1, i] = coordinatesY[i].V();
            }

            // Finally populate the values matrix with the provided data.
            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    this[x, y] = values[x, y].V();
                }
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the data inside the data structures
        /// after doing checks for consistency.
        /// </summary>
        /// <exception cref="Exception">
        /// If the data is not valid. For example the coordinates 
        /// values aren't ordered from lesser to greater.
        /// </exception>
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
        /// <returns>The requested value at the position.</returns>
        private double this[int x, int y]
        {
            get
            {
                // If both are > -1 it means it's a value in the main matrix.
                // If one is -1 it means to get the coordinate values.
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

                // None of the previous attempts worked so just return 0.
                return 0;
            }

            set
            {
                if (y > -1 && x > -1)
                {
                    this.values[x, y] = value;
                }
                else if (y != -1)
                {
                    // Check if the y coordinates are ordered from lower to greater.
                    // else throw an error.
                    if ((y > 0 && this.coordinatesY[y - 1] > value) ||
                       (y < this.coordinatesY.Count - 1 && this.coordinatesY[y + 1] < value))
                    {
                        throw new Exception("Function integrity wasn't maintained in the " +
                                            "y coordinates");
                    }

                    this.coordinatesY[y] = value;
                }
                else if (x != -1)
                {
                    // Check if the x coordinates are ordered from lower to greater.
                    // else throw an error.
                    if ((x > 0 && this.coordinatesX[x - 1] > value) ||
                       (x < this.coordinatesX.Count - 1 && this.coordinatesX[x + 1] < value))
                    {
                        throw new Exception("Function integrity wasn't maintained in the " +
                                            "x coordinates");
                    }

                    this.coordinatesX[x] = value;
                }
            }
        }

        #endregion Properties

        #region Getters and Setters

        /// <summary>
        /// Sets the sizes of the data structures which will be used.
        /// </summary>
        /// <param name="x">The x length.</param>
        /// <param name="y">The y length.</param>
        private void SetSizes(int x, int y)
        {
            this.coordinatesX.Resize(x);
            this.coordinatesY.Resize(y);
            this.values.NewSize(x, y);
        }

        #endregion Getters and Setters

        #region Internal functions

        /// <summary>
        /// Finds the position of a coordinate in a <see cref="IRightValue"/>,
        /// it's used with coordinatesX and coordinatesY.
        /// </summary>
        /// <param name="posVector">
        /// A reference to the <see cref="Vector"/> to use to find the required item index.
        /// </param>
        /// <param name="value">The value to search for, exact search.</param>
        /// <returns>
        /// The position in the given vector of the given value. The first element has index 0.
        /// -1 is returned in case it wasn't possible to find the value inside the given vector.
        /// </returns>
        private int FindPosition(ref Vector posVector, double value)
        {
            for (int i = 0; i < posVector.Count; i++)
            {
                if (posVector[i] == value)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Finds the nearest index before the given one.
        /// </summary>
        /// <param name="posVector">
        /// A reference to the <see cref="IRightValue"/> to use to find the required item index.
        /// </param>
        /// <param name="value">
        /// The value to search for the nearest lower element to the given one.
        /// </param>
        /// <returns>
        /// The position in the given <see cref="IRightValue"/> of the nearest 
        /// value before the requested one.
        /// The first element has index 0.
        /// </returns>
        private int FindNearestBefore(ref Vector posVector, double value)
        {
            for (int i = 0; i < posVector.Count; i++)
            {
                if (posVector[i] > value)
                    return i - 1;
            }

            return posVector.Count - 1;
        }

        /// <summary>
        /// Calculates the value at the requested coordinates through linear
        /// interpolation having to interpolate only through a single direction.
        /// </summary>
        /// <remarks>
        /// The function is handled directly from <see cref="CalculateLinear"/>
        /// in the cases bilinear interpolation is not applicable (due
        /// to collapsed coordinates because of lack of data or being at
        /// the boundaries).
        /// </remarks>
        /// <param name="x">The coordinate to calculate (in the single dimension).</param>
        /// <param name="x0">The coordinate before the one to calculate that it's known.</param>
        /// <param name="x1">The coordinate after the one to calculate th at it's known.</param>
        /// <param name="y0">The value of the function at the position x0.</param>
        /// <param name="y1">The value of the function at the position x1.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateLinearSingle(double x, double x0, double x1, double y0, double y1)
        {
            return y0 + ((((x - x0) * y1) - ((x - x0) * y0)) / (x1 - x0));
        }

        /// <summary>
        /// Calculates the value at the requested coordinates through linear
        /// interpolation.
        /// </summary>
        /// <remarks>
        /// The value requested must not be at the margin of the <see cref="matrix"/>,
        /// so there must be at least one entry under and on the left of the
        /// requested value.</remarks>
        /// <param name="x">The x coordinate where to calculate the value.</param>
        /// <param name="y">The y coordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateLinear(double x, double y)
        {
            // The indices we will find here will always before the last, so there is no need to
            // do bound checking for this function.
            int beforeX = FindNearestBefore(ref this.coordinatesX, x);
            int beforeY = FindNearestBefore(ref this.coordinatesY, y);

            // Check if this is a situation which would end up going at the edge of bounds
            // due to lack of data.
            if (beforeX == this.coordinatesX.Count - 1)
            {
                // In this case we do a linear interpolation only over the y axis.
                return CalculateLinearSingle(y, this.coordinatesY[beforeY],
                                             this.coordinatesY[beforeY + 1],
                                             this.values[beforeX, beforeY],
                                             this.values[beforeX, beforeY + 1]);
            }

            if (beforeY == this.coordinatesY.Count - 1)
            {
                // In this case we do a linear interpolation only over the x axis.
                return CalculateLinearSingle(x, this.coordinatesX[beforeX],
                                             this.coordinatesX[beforeX + 1],
                                             this.values[beforeX, beforeY],
                                             this.values[beforeX + 1, beforeY]);
            }

            // The denominator of the formula which gives the linear interpolation.
            // (x2 - x1) * (y2 - y1)
            double denominator = (this.coordinatesX[beforeX + 1] - this.coordinatesX[beforeX]) *
                             (this.coordinatesY[beforeY + 1] - this.coordinatesY[beforeY]);

            // The 4 factors of the formula providing the linear interpolation.
            double factor1 = (this.values[beforeX, beforeY] / denominator) *
                             ((this.coordinatesX[beforeX + 1] - x) * (this.coordinatesY[beforeY + 1] - y));

            double factor2 = (this.values[beforeX + 1, beforeY] / denominator) *
                             ((x - this.coordinatesX[beforeX]) * (this.coordinatesY[beforeY + 1] - y));

            double factor3 = (this.values[beforeX, beforeY + 1] / denominator) *
                             ((this.coordinatesX[beforeX + 1] - x) * (y - this.coordinatesY[beforeY]));

            double factor4 = (this.values[beforeX + 1, beforeY + 1] / denominator) *
                             ((x - this.coordinatesX[beforeX]) * (y - this.coordinatesY[beforeY]));

            // Finally sum the 4 factors together and return the result.
            return factor1 + factor2 + factor3 + factor4;
        }

        /// <summary>
        /// Calculates the spline interpolation from left, which is just
        /// the nearest value available before the requested one (left/up).
        /// </summary>
        /// <param name="x">The x coordinate where to calculate the value.</param>
        /// <param name="y">The y coordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateSpline(double x, double y)
        {
            // Not implemented.
            return 0;
        }

        /// <summary>
        /// Calculates the constant interpolation from left, which is just
        /// the nearest value available before the requested one (left/up).
        /// </summary>
        /// <param name="x">The x coordinate where to calculate the value.</param>
        /// <param name="y">The y coordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateConstantBefore(double x, double y)
        {
            int selectedX = FindNearestBefore(ref this.coordinatesX, x);
            int selectedY = FindNearestBefore(ref this.coordinatesY, y);

            return this.values[selectedX, selectedY];
        }

        /// <summary>
        /// Calculates the constant interpolation from right, which is just
        /// the nearest value available before the requested one (right/down).
        /// </summary>
        /// <param name="x">The x coordinate where to calculate the value.</param>
        /// <param name="y">The y coordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateConstantAfter(double x, double y)
        {
            int selectedX = FindNearestBefore(ref this.coordinatesX, x);
            int selectedY = FindNearestBefore(ref this.coordinatesY, y);

            // In case the nearest before is actually a boundary cell
            // apply a correction to the index in order to take the last
            // value available at the end of the data.
            if (selectedX == this.coordinatesX.Count - 1)
            {
                selectedX--;
            }

            if (selectedY == this.coordinatesY.Count - 1)
            {
                selectedY--;
            }

            return this.values[selectedX + 1, selectedY + 1];
        }

        #endregion Internal functions

        #region Public functions

        /// <summary>
        /// Copies the data inside this <see cref="CPointFunction2D"/>
        /// into another <see cref="CPointFunction2D"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="CPointFunction2D"/> where to copy the data to.
        /// </param>
        internal void CopyTo(CPointFunction2D other)
        {
            other.SetSizes(this.coordinatesX.Count, this.coordinatesY.Count);
            this.coordinatesX.CopyTo(other.coordinatesX);
            this.coordinatesY.CopyTo(other.coordinatesY);
            this.values.CopyTo(other.values);
            other.interpolationType = this.interpolationType;
            other.extrapolationType = this.extrapolationType;
        }

        /// <summary>
        /// Evaluates the function at the requested x and y point,
        /// using, if necessary, an interpolation.
        /// </summary>
        /// <param name="x">The x coordinate where to evaluate the function.</param>
        /// <param name="y">The y coordinate where to calculate the value.</param>
        /// <returns>The value of the function at the requested point.</returns>
        internal double Evaluate(double x, double y)
        {
            // First of all check if we have any data
            if (this.coordinatesX.Count == 0 || this.coordinatesY.Count == 0)
            {
                return 0;
            }

            // Values outside the range of the x and y aren't allowed
            // in those case the last value in the boundary direction
            // which was exceeded is returned. (Extrapolation)
            // Note: The bounds are determined by the first and last element
            //       of the <see cref="Vector"/> as they represent ordered indexes.
            if (x < this.coordinatesX[0])
            {
                x = this.coordinatesX[0];
            }
            else if (x > this.coordinatesX[this.coordinatesX.Count - 1])
            {
                x = this.coordinatesX[this.coordinatesX.Count - 1];
            }

            if (y < this.coordinatesY[0])
            {
                y = this.coordinatesY[0];
            }
            else if (y > this.coordinatesY[this.coordinatesY.Count - 1])
            {
                y = this.coordinatesY[this.coordinatesY.Count - 1];
            }

            // Bounds check is now done, so it's possible to fetch the requested value.

            // First search for the exact position, if already known. This will
            // also handle, as a result, the case in which the requested value is at
            // the margin of the matrix, so it will never be possible the code
            // which handles interpolation will have to handle this case.
            int selectedX = FindPosition(ref this.coordinatesX, x);
            int selectedY = FindPosition(ref this.coordinatesY, y);
            if (selectedX != -1 && selectedY != -1)
            {
                // In this case the requested position was actually provided
                // so it's enough to just return it.
                return this.values[selectedX, selectedY];
            }
            else
            {
                // If it wasn't possible to find the requested position
                // try to interpolate it with the requested interpolation method.
                switch (this.interpolationType)
                {
                    // A linear interpolation.
                    case EInterpolationType.LINEAR:
                        return CalculateLinear(x, y);

                    // A spline interpolation.
                    case EInterpolationType.SPLINE:
                        return CalculateSpline(x, y);

                    // A constant interpolation (zero order left)
                    // It interpolates by taking the left up values.
                    case EInterpolationType.ZERO_ORDER_LEFT:
                        return CalculateConstantBefore(x, y);

                    // A constant interpolation (zero order)
                    // It interpolates by taking the right down values.
                    case EInterpolationType.ZERO_ORDER:
                        return CalculateConstantAfter(x, y);

                    // Any interpolation type which isn't supported will return zero.
                    default:
                        return 0;
                }
            }
        }

        #endregion Public functions
    }
}
