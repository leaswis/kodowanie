using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kodowanieM
{
    class CompressedBlock
    {
        public static SortedDictionary<byte, double> Count(byte[] fileInBytes)
        {

            SortedDictionary<byte, double> countedC = new SortedDictionary<byte, double>();



            foreach (var c in fileInBytes)
            {
                if (!countedC.ContainsKey(c))
                {
                    countedC.Add(c, 1);
                }
                else
                {
                    countedC[c]++;
                }
            }

            return countedC;
        }
    }
    }

