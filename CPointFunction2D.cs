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
    /// It provides the capabilities to rappresent a matrix and give to it
    /// cordinates, additionally it allows to interpolate values between the values
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
        /// The x cordinates for the matrix.
        /// </summary>
        private Vector positionsX;

        /// <summary>
        /// The y cordinates for the matrix.
        /// </summary>
        private Vector positionsY;

        /// <summary>
        /// Defines the interpolation technique to apply, if required.
        /// </summary>
        private EInterpolationType interpolationType;

        #endregion Function Parameters

        public void fillsomedata()
        {
            SetSizes(5, 5);
            this.positionsX[0] = 1;
            this.positionsX[1] = 20;
            this.positionsX[2] = 30;
            this.positionsX[3] = 50;
            this.positionsX[4] = 100;
            this.positionsY[0] = 1;
            this.positionsY[1] = 40;
            this.positionsY[2] = 70;
            this.positionsY[3] = 100;
            this.positionsY[4] = 1000;
            this.values[0, 0] = 145;
            this.values[0, 1] = 45;
            this.values[0, 2] = 90;
            this.values[0, 3] = 25;
            this.values[0, 4] = 253;

            this.values[1, 0] = 345;
            this.values[1, 1] = 95;
            this.values[1, 2] = 100;
            this.values[1, 3] = 275;
            this.values[1, 4] = 233;

            this.values[2, 0] = 45;
            this.values[2, 1] = 5;
            this.values[2, 2] = 0;
            this.values[2, 3] = 75;
            this.values[2, 4] = 33;

            this.values[3, 0] = 545;
            this.values[3, 1] = 55;
            this.values[3, 2] = 50;
            this.values[3, 3] = 575;
            this.values[3, 4] = 533;

            this.values[4, 0] = 1545;
            this.values[4, 1] = 15;
            this.values[4, 2] = 150;
            this.values[4, 3] = 1575;
            this.values[4, 4] = 533;
            Console.WriteLine(this.values.ToString());
            Console.WriteLine(this.positionsX.ToString());
            Console.WriteLine(this.positionsY.ToString());
        }

        #region Constructors

        /// <summary>
        /// Default constructor, just initializes the data storage.
        /// </summary>
        public CPointFunction2D()
        {
            this.values = new Matrix();
            this.positionsX = new Vector();
            this.positionsY = new Vector();
            this.interpolationType = EInterpolationType.LINEAR;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the vector of the x cordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any cordinate.
        /// </remarks>
        internal Vector PositionsX
        {
            get
            {
                return this.positionsX;
            }
        }

        /// <summary>
        /// Gets the vector of the y cordinates in the data.
        /// </summary>
        /// <remarks>
        /// To be used only internally, use Evaluate to get the value at any cordinate.
        /// </remarks>
        internal Vector PositionsY
        {
            get
            {
                return this.positionsY;
            }
        }

        internal double this[int x, int y]
        {
            get
            {
                if (y != -1 && x != -1)
                {
                    return this.values[x, y];
                }
                else if (y != -1)
                {
                    return this.positionsY[y];
                }
                else if (x != -1)
                {
                    return this.positionsX[x];
                }

                return 0;
            }

            set
            {
                if (y != -1 && x != -1)
                {
                    this.values[x, y] = value;
                }
                else if (y != -1)
                {
                    if ((y > 0 && this.positionsY[y - 1] > value) ||
                       (y < this.positionsY.Count - 1 && this.positionsY[y + 1] < value))
                    {
                        throw new Exception("Function integrity wasn't maintained in the y parameters.");
                    }

                    this.positionsY[y] = value;
                }
                else if (x != -1)
                {
                    if ((x > 0 && this.positionsX[x - 1] > value) ||
                       (x < this.positionsX.Count - 1 && this.positionsX[x + 1] < value))
                    {
                        throw new Exception("Function integrity wasn't maintained in the x parameters.");
                    }

                    this.positionsX[x] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the interpolation to use in case the value request
        /// is not already available.
        /// </summary>
        /// <remarks>
        /// To be used only internally, in order to set the desidered interpolation and
        /// get what the current one.
        /// </remarks>
        internal EInterpolationType Interpolation
        {
            get
            {
                return this.interpolationType;
            }

            set
            {
                this.interpolationType = value;
            }
        }

        #endregion Properties

        #region Getters and Setters

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
            return this.values[x, y];
        }

        internal void SetPointValue(int x, int y, double value)
        {
            this.values[x, y] = value;
        }

        internal void SetPointValue(int x, int y, RightValue value)
        {
            this.values[x, y] = value.fV();
        }

        internal void SetSizes(int x, int y)
        {
            this.positionsX.Resize(x);
            this.positionsY.Resize(y);
            this.values.NewSize(x, y);
        }

        #endregion Getters and Setters

        #region Internal functions

        /// <summary>
        /// Finds the position of a cordinate in a vector,
        /// it's used with positionsX and positionsY.
        /// </summary>
        /// <param name="posVector">
        /// A reference to the vector to use to find the required item index.
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
        /// A reference to the vector to use to find the required item index.
        /// </param>
        /// <param name="value">
        /// The value to search for the nearest lower element to the given one.
        /// </param>
        /// <returns>
        /// The position in the given vector of the nearest value before the requested one.
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
        /// Calculates the value at the requested cordinates through linear
        /// interpolation.
        /// </summary>
        /// <remarks>
        /// The value requested must not be at the margin of the matrix,
        /// so there must be at least one entry under and on the left of the
        /// requested value.</remarks>
        /// <param name="x">The x cordinate where to calculate the value.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateLinear(double x, double y)
        {
            // The indices we will find here will always before the last, so there is no need to
            // do bound checking for this function.
            int beforeX = FindNearestBefore(ref this.positionsX, x);
            int beforeY = FindNearestBefore(ref this.positionsY, y);

            // The denominator of the formula which gives the linear interpolation.
            // (x2 - x1) * (y2 - y1)
            double denominator = (this.positionsX[beforeX + 1] - this.positionsX[beforeX]) *
                             (this.positionsY[beforeY + 1] - this.positionsY[beforeY]);

            // The 4 factors of the formula providing the linear interpolation.
            double factor1 = (this.values[beforeX, beforeY] / denominator) *
                             ((this.positionsX[beforeX + 1] - x) * (this.positionsY[beforeY + 1] - y));

            double factor2 = (this.values[beforeX + 1, beforeY] / denominator) *
                             ((x - this.positionsX[beforeX]) * (this.positionsY[beforeY + 1] - y));

            double factor3 = (this.values[beforeX, beforeY + 1] / denominator) *
                             ((this.positionsX[beforeX + 1] - x) * (y - this.positionsY[beforeY]));

            double factor4 = (this.values[beforeX + 1, beforeY + 1] / denominator) *
                             ((x - this.positionsX[beforeX]) * (y - this.positionsY[beforeY]));

            // Finally sum the 4 factors togheter and return the result.
            return factor1 + factor2 + factor3 + factor4;
        }

        private double CalculateSpline(double x, double y)
        {
            return 0;
        }

        /// <summary>
        /// Calculates the constant interpolation from left, which is just
        /// the nearest value available before the requested one (left/up).
        /// </summary>
        /// <param name="x">The x cordinate where to calculate the value.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateConstantBefore(double x, double y)
        {
            int selectedX = FindNearestBefore(ref this.positionsX, x);
            int selectedY = FindNearestBefore(ref this.positionsY, y);

            return this.values[selectedX, selectedY];
        }

        /// <summary>
        /// Calculates the constant interpolation from right, which is just
        /// the nearest value available before the requested one (right/down).
        /// </summary>
        /// <param name="x">The x cordinate where to calculate the value.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateConstantAfter(double x, double y)
        {
            int selectedX = FindNearestBefore(ref this.positionsX, x);
            int selectedY = FindNearestBefore(ref this.positionsY, y);

            // TODO: check bounds.
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
            other.SetSizes(this.positionsX.Count, this.positionsY.Count);
            this.positionsX.CopyTo(other.positionsX);
            this.positionsY.CopyTo(other.positionsY);
            this.values.CopyTo(other.values);
        }

        /// <summary>
        /// Evaluates the function at the requested x and y point,
        /// using, if necessary, an interpolation.
        /// </summary>
        /// <param name="x">The x cordinate where to evaluate the function.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The value of the function at the requested point.</returns>
        internal double Evaluate(double x, double y)
        {
            // First of all check if we have any data
            if (this.positionsX.Count == 0)
            {
                return 0;
            }

            // Values outside the range of the x and y aren't allowed
            // in those case the last value in the bounduary direction
            // which was exceeded is returned. (Extrapolation)
            // Note: The bounds are determined by the first and last element
            //       of the vectors as they rappresent ordered indexes.
            if (x < this.positionsX[0])
            {
                x = this.positionsX[0];
            }
            else if (x > this.positionsX[this.positionsX.Count - 1])
            {
                x = this.positionsX[this.positionsX.Count - 1];
            }

            if (y < this.positionsY[0])
            {
                y = this.positionsY[0];
            }
            else if (y > this.positionsY[this.positionsY.Count - 1])
            {
                y = this.positionsY[this.positionsY.Count - 1];
            }

            // Bounds check is now done, so it's possible to fetch the requested value.

            // First search for the exact position, if already known. This will
            // also handle, as a result, the case in which the requested value is at
            // the margin of the matrix, so it will never be possible the code
            // which handles interpolation will have to handle this case.
            int selectedX = FindPosition(ref this.positionsX, x);
            int selectedY = FindPosition(ref this.positionsY, y);
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

                    // Any interpolation type which in't supported will return zero.
                    default:
                        return 0;
                }
            }
        }

        #endregion Public functions
    }
}
