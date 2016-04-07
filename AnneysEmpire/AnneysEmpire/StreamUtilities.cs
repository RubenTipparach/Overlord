using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
    public static class StreamUtilities
    {
        /// <summary>
        /// Only supports singular array at the moment, dumping complex data table is on my todo list.
        /// </summary>
        /// <param name="filePathName"></param>
        /// <param name="dataArray"></param>
        public static void DumpData(String filePathName, double[] dataArray)
        {
            var outDefault = Console.Out;

            using (StreamWriter writer = new StreamWriter(filePathName))
            {
                Console.SetOut(writer);
                foreach(var d in dataArray)
                {
                    Console.WriteLine(d.ToString());
                }
            }

            Console.SetOut(outDefault);
        }
    }
}
