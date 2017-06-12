/* Copyright (C) 2013 Fairmat SRL (info@fairmat.com, http://www.fairmat.com/)
 * Author(s): Matteo Tesser (matteo.tesser@fairmat.com)
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
namespace DatesGenerator
{
 
    /// <summary>
    /// Defines Freq2Period user function
    /// </summary>
    [Mono.Addins.Extension("/Fairmat/UIFunction")]
    public class Freq2Period : IUIFunction
    {
        double func(double x)
        {
            DateFrequency df = (DateFrequency)(int)x;
            return DateFrequency2YearFraction(df);
        }

       public static double DateFrequency2YearFraction(DateFrequency df)
        {
            switch (df)
            {
                case DateFrequency.Annual:
                    return 1;
                case DateFrequency.Semiannual:
                    return 0.5;
                case DateFrequency.Monthly:
                    return 1.0 / 12;
                case DateFrequency.EveryTwoMonths:
                    return 2.0 / 12;
                case DateFrequency.ThreePerAnnum:
                    return 4.0 / 12;
                case DateFrequency.Quarterly:
                    return 3.0 / 12;
                case DateFrequency.BiWeekly:
                    return 2.0 / 52;
                case DateFrequency.Weekly:
                    return 1.0 / 52;
                case DateFrequency.Daily:
                    return 1.0 / 365;
                case DateFrequency.EveryTwoYears:
                    return 2;
                case DateFrequency.EveryThreeYears:
                    return 3;
                default:
                    throw new Exception("Freq2Period: wrong input frequency!");
            }
        }

        #region IUIFunction Members
        public Delegate Function
        {
            get { return new TAEDelegateFunction1D(func); }
        }
        #endregion

        #region ISymbolDefinition Members
        public string Name
        {
            get { return "Freq2Period"; }
        }
        public string Description
        {
            get { return "Converts a Symbolic Frequency to a period expressed in year fractions. For example Monthly -> 1/12"; }
        }
        #endregion
    }
}
