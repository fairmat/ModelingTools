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
using DVPLDOM;
using DVPLI;
using DVPForms;

namespace PFunction2D
{
    public class PFunction2D : Function, IEditable
    {
        public PFunction2D(Project context)
            : base(EModelParameterType.POINT_FUNCTION, context)
        {
            CPointFunction2D a = new CPointFunction2D();
            a.fillsomedata();
            Console.WriteLine(a.Evaluate(50, 100));
            Console.WriteLine(a.Evaluate(55, 105));
            DVPForms.EditFunctionsForm b = new DVPForms.EditFunctionsForm();
            b.ShowDialog();
        }

        static void Main(string[] args)
        {
            CPointFunction2D a = new CPointFunction2D();
            a.fillsomedata();
            Console.WriteLine(a.Evaluate(50, 100));
            Console.WriteLine(a.Evaluate(55, 105));
            DVPForms.EditFunctionsForm b = new DVPForms.EditFunctionsForm();
            b.ShowDialog();
        }
    }
}
