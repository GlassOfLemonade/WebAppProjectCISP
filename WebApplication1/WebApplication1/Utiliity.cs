using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtSac.LabFinal
{
    public static class Utility
    {

        public static Decimal TotalSales = 1000000M;

        public static Int32 TryParseEx(this String value)
        {
            Int32 ix;
            Int32.TryParse(value, out ix);
            return ix;
        }

        public static bool IsEven(this Int32 value)
        {
            return (value % 2) == 0;
        }


    }
}
