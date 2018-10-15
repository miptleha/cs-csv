Read, write data to file/stream in csv format.  
This is simplest implementation of database.  
Data can be stored in string, on filesystem or in any stream.  

 File | Description
 --- | --- 
 [Csv.cs](Csv.cs) | Methods for loading/saving csv from file/stream
 [TableData.cs](TableData.cs) | Classes for storing cvs
 [Comparer.cs](Comparer.cs) (bonus) | Compare content of objects with user-friendly output where differs
 [Program.cs](Program.cs) | Test application 

Open project in Visual Studio and run

## Sample code

```cs
//test data
var data = new TableData();
data.Header.AddRange(new string[] { "col1", "col2", "col3", "col4", "123" });
for (int i = 0; i < 10; i++)
{
    string prefix = "val" + i;
    data.Content.Add(new string[] { prefix + "1", prefix + "2", prefix + "3", prefix + "4" });
}

//save to stream
var ms = new MemoryStream();
Csv.WriteCsv(ms, data);

//save to file
Csv.WriteCsv(path, data);

//save to string
string csvData = Csv.WriteCsvString(data);

//load from stream
ms.Position = 0;
var data1 = Csv.ReadCsv(ms);

//load from file
var data2 = Csv.ReadCsv(path);

//load from string
var data3 = Csv.ReadCsvString(csvData);
```
