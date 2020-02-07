using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline_test
{
    static class StringConc
    {
        static StringBuilder stringBuilder;
        public static List<string> stringList;

        public static void Initialize()
        {
            stringBuilder = new StringBuilder();
        }
 
        public static string ConcString()
        {
            stringBuilder.Clear();
            for (int i = 0; i < stringList.Count(); i++)
            {
                stringBuilder.Append(stringList[i]);
            }

            return stringBuilder.ToString();           
        }
    }
}
