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
        public static TableData ReadCsv(string path)
        {
            if (!File.Exists(path))
                return null;

            using (var fs = File.OpenRead(path))
            {
                return ReadCsv(fs);
            }
        }

        public static void WriteCsv(string path, TableData data)
        {
            using (var fs = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                WriteCsv(fs, data);
            }
        }

        public static TableData ReadCsv(Stream ms)
        {
            var data = new TableData();
            StreamReader sr = new StreamReader(ms, Encoding.UTF8);
            string s = sr.ReadLine();
            if (s != null)
                data.Header.AddRange(s.Split(new string[] { ", " }, StringSplitOptions.None));
            while ((s = sr.ReadLine()) != null)
            {
                data.Content.Add(s.Split(new string[] { ", " }, StringSplitOptions.None));
            }
            return data;
        }

        public static void WriteCsv(Stream ms, TableData data)
        {
            StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
            sw.WriteLine(String.Join(", ", data.Header));
            foreach (var r in data.Content)
            {
                sw.WriteLine(String.Join(", ", r));
            }
            sw.Flush();
        }
    }
}
