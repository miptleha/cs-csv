using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CsvInOut
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var data = new TableData();
                data.Header.AddRange(new string[] { "col1", "col2", "col3", "col4", "123" });
                for (int i = 0; i < 10; i++)
                {
                    string prefix = "val" + i;
                    data.Content.Add(new string[] { prefix + "1", prefix + "2", prefix + "3", prefix + "4" });
                }
                data.Content.Add(new string[] { "\"", "'", ", ", ", ", "a" });

                var ms = new MemoryStream();
                Csv.WriteCsv(ms, data);

                ms.Position = 0;
                WriteToConsole(ms);

                ms.Position = 0;
                var data1 = Csv.ReadCsv(ms);

                string res = Comparer.Compare(data, data1);
                if (res == null)
                    Console.WriteLine("Test passed");
                else
                    Console.WriteLine("Test failed: " + res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void WriteToConsole(MemoryStream ms)
        {
            var reader = new StreamReader(ms);
            string line;
            while ((line = reader.ReadLine()) != null)
                Console.WriteLine(line);
            Console.WriteLine();
        }
    }
}
