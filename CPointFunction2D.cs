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
using System.Linq;
using System.Text;
using DVPLI;

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

        public void fillsomedata()
        {
            this.positionsX.Resize(5);
            this.positionsX[0] = 1;
            this.positionsX[1] = 20;
            this.positionsX[2] = 30;
            this.positionsX[3] = 50;
            this.positionsX[4] = 100;
            this.positionsY.Resize(5);
            this.positionsY[0] = 1;
            this.positionsY[1] = 40;
            this.positionsY[2] = 70;
            this.positionsY[3] = 100;
            this.positionsY[4] = 1000;
            this.values.NewSize(5, 5);
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

        /// <summary>
        /// Default constructor, just initializes the data storage.
        /// </summary>
        public CPointFunction2D()
        {
            this.values = new Matrix();
            this.positionsX = new Vector();
            this.positionsY = new Vector();
        }

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
        /// <param name="x">The x cordinate where to calculate the value.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateLinear(double x, double y)
        {
            int beforeX = FindNearestBefore(ref this.positionsX, x);
            int beforeY = FindNearestBefore(ref this.positionsY, y);

            // The denominator of the formula which gives the linear interpolation.
            // (x2 - x1) * (y2-y1)
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

        /// <summary>
        /// Calculates the constant interpolation, which is just
        /// the nearest value available.
        /// </summary>
        /// <param name="x">The x cordinate where to calculate the value.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The calculated value.</returns>
        private double CalculateConstant(double x, double y)
        {
            int selectedX = FindNearestBefore(ref this.positionsX, x);
            int selectedY = FindNearestBefore(ref this.positionsY, y);

            return this.values[selectedX, selectedY];
        }

        /// <summary>
        /// Evaluates the function at the requested x and y point,
        /// using, if necessary, an interpolation.
        /// </summary>
        /// <param name="x">The x cordinate where to evaluate the function.</param>
        /// <param name="y">The y cordinate where to calculate the value.</param>
        /// <returns>The value of the function at the requested point.</returns>
        public double Evaluate(double x, double y)
        {
            // First search for the exact position, if already known.
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
                // try to interpolate it.
                return CalculateLinear(x, y);
            }
        }
    }
}
