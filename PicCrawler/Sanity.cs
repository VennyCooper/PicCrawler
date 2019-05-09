using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicCrawler
{
    public static class Sanity
    {
        public static void Requires(bool conditionSatisfied, string errMsg)
        {
            if (!conditionSatisfied)
            {
                throw new Exception(errMsg);
            }
        }

        public static void Requires(bool conditionSatisfied, Exception e)
        {
            if (!conditionSatisfied)
            {
                throw e;
            }
        }
    }
}
