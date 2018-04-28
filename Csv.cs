using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvInOut
{
    class Csv
    {
        public static TableData ReadCsv(MemoryStream ms)
        {
            var data = new TableData();
            StreamReader sr = new StreamReader(ms);
            string s = sr.ReadLine();
            if (s != null)
                data.Header.AddRange(s.Split(new string[] { ", " }, StringSplitOptions.None));
            while ((s = sr.ReadLine()) != null)
            {
                data.Content.Add(s.Split(new string[] { ", " }, StringSplitOptions.None));
            }
            return data;
        }

        public static MemoryStream WriteCsv(TableData data)
        {
            var ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.WriteLine(String.Join(", ", data.Header));
            foreach (var r in data.Content)
            {
                sw.WriteLine(String.Join(", ", r));
            }
            sw.Flush();
            return ms;
        }
    }
}
