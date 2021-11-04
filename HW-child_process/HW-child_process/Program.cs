using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;


namespace HW_child_process
{
    class Program
    {
        static void Main()
        {
            const string path = @"C:\Program Files\Google\Chrome\Application\Chrome.exe";
            const string site = "https://translate.google.ru";
            Process.Start(path, site);

            var procInfo1 = new ProcessStartInfo
            {
                FileName = "notepad",
                Arguments = @"G:\STEP\systems-progr\qq.txt"
            };
            Process.Start(procInfo1);

            var procInfo2 = new ProcessStartInfo
            {
                FileName = @"C:\Program Files\Microsoft Office\root\Office16\EXCEL.EXE",
                Arguments = @"G:\STEP\systems-progr\newExcel.xlsx"
            };
            Process.Start(procInfo2);

            
            ShowProcessesByName("notepad");
            ShowProcessesByName("C://Program Files//Google//Chrome//Application//Chrome.exe");
            
            static void ShowProcessesByName(string name)
            {
                var processes = Process.GetProcessesByName(name);
                foreach (var process in processes)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{process.Id}: ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{process.ProcessName} - ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"{process.StartTime}");
                    Console.ResetColor();
                }
            }

        }
    }
}


/*
 *var fileName = @"G:\STEP\systems-progr\newExcel.xlsx";
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
            using (var conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                var sheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] ";

                    var adapter = new OleDbDataAdapter(cmd);
                    var ds = new DataSet();
                    adapter.Fill(ds);
                }
            }
*/