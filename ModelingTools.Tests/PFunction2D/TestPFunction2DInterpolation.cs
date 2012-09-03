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

using DVPLI;
using DVPLUtils;
using NUnit.Framework;

namespace PFunction2D
{
    /// <summary>
    /// Tests PFunction2D Interpolations.
    /// </summary>
    [TestFixture]
    public class TestPFunction2DInterpolation
    {
        /// <summary>
        /// Initializes the backend to run the tests.
        /// </summary>
        [SetUp]
        public void Init()
        {
            TestCommon.TestInitialization.CommonInitialization();
        }

        /// <summary>
        /// Setups some sample data:
        ///   x  0 100
        ///   0  0  50
        /// 100 50 100
        /// in a PFunction2D.
        /// </summary>
        /// <returns>A PFunction2D populated with the sample data.</returns>
        private PFunction2D SetTestData1()
        {
            // Prepare some simple data.
            Vector coordinatesX = new Vector(2);
            Vector coordinatesY = new Vector(2);
            Matrix values = new Matrix(2);

            coordinatesX[0] = 0.0;
            coordinatesX[1] = 100.0;

            coordinatesY[0] = 0.0;
            coordinatesY[1] = 100.0;

            values[0, 0] = 0.0;
            values[0, 1] = 50.0;
            values[1, 1] = 100.0;
            values[1, 0] = 50.0;

            return new PFunction2D(coordinatesX, coordinatesY, values);
        }

        /// <summary>
        /// Tries the linear interpolation at point 50, 50
        /// with the test data 1. In that point the result should be 50.
        /// </summary>
        [Test]
        public void TestLinearInterpolation1()
        {
            PFunction2D func = SetTestData1();
            func.Interpolation = EInterpolationType.LINEAR;
            func.Parse(null);
            Assert.AreEqual(50, func.Evaluate(50, 50));
        }

        /// <summary>
        /// Tries the Zero Order interpolation at point 50, 50
        /// with the test data 1. In that point the result should be 100
        /// as it should take the point on the bottom right, which is valued as 100.
        /// </summary>
        [Test]
        public void TestZeroOrderInterpolation1()
        {
            PFunction2D func = SetTestData1();
            func.Interpolation = EInterpolationType.ZERO_ORDER;
            func.Parse(null);
            Assert.AreEqual(100, func.Evaluate(50, 50));
        }

        /// <summary>
        /// Tries the Left Zero Order interpolation at point 50, 50
        /// with the test data 1. In that point the result should be 0
        /// as it should take the point on the upper left, which is valued as 0.
        /// </summary>
        [Test]
        public void TestZeroOrderLeftInterpolation1()
        {
            PFunction2D func = SetTestData1();
            func.Interpolation = EInterpolationType.ZERO_ORDER_LEFT;
            func.Parse(null);
            Assert.AreEqual(0, func.Evaluate(50, 50));
        }
    }
}
