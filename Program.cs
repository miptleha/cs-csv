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
                data.Content.Add(new string[] { "\"", "; ; d;;;;;", "here \" character", "; character" });

                var ms = new MemoryStream();
                Csv.WriteCsv(ms, data);

                ms.Position = 0;
                WriteToConsole(ms);

                ms.Position = 0;
                var data1 = Csv.ReadCsv(ms);
                Compare("WriteCsv/ReadCsv (memory stream)", data, data1);

                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "test.csv");
                Csv.WriteCsv(path, data);
                var data2 = Csv.ReadCsv(path);
                Compare("WriteCsv/ReadCsv (file)", data, data2);

                string csvData = Csv.WriteCsvString(data);
                var data3 = Csv.ReadCsvString(csvData);
                Compare("WriteCsvString/ReadCsvString (string)", data, data3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Compare(string test, TableData src, TableData dst)
        {
            string res = Comparer.Compare(src, dst);
            if (res == null)
                Console.WriteLine("Test " + test + " passed");
            else
                Console.WriteLine("Test " + test + " failed: " + res);
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
