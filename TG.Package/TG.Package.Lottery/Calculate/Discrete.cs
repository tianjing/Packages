using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Package.Lottery.Calculate
{
    public class Discrete
    {
        /// <summary>
        /// Permutation ( ex: P(8,6) or P(6,6) )
        /// </summary>
        public static Int32 P(Int32 n, Int32 m)
        {
            if (m > n && m <= 0 && n <= 0)
            { throw new FormatException("error:m is Larger than n"); }

                Int32 result = 1;
                for (var i = 0; i < m; i++)
                {
                    try
                    {
                        checked
                        {
                            result = result * (n - i);
                        }
                    }
                    catch
                    {
                        throw new FormatException("error:result is Larger than Int32.MaxValue");
                    }
                }
                return result;
        }
        /// <summary>
        /// Combination ( ex: C(8,6) or C(6,6) )
        /// </summary>
        public static Int32 C(Int32 n, Int32 m)
        {
            if (n == m)
            {
                return 1;
            }
            return P(n, m) / P(m, m);
        }

    }
}
