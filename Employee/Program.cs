using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Employee
{

    class Program
    {

        private static string[][] data;
        private static Dictionary<string, int> workingTimeTogether ;

        private static DateTime StringToDate(string temp)
        {
            if (temp.ToLower() == "null")
                return DateTime.Now;

            return DateTime.Parse(temp);
        }

        private static bool LoadData()
        {
            Console.WriteLine("Write absolute path to txt file.");
            string testFileName = Console.ReadLine();
            if (testFileName == null || !File.Exists(testFileName))
            {
                Console.WriteLine("File Not Found.");
                return false;
            }

            workingTimeTogether = new Dictionary<string, int>();
            data = File.ReadAllLines(testFileName).Select(x => x.Split(',').Select(z => z.Trim()).ToArray()).ToArray();

            return true;
        }

        static void Main(string[] args)
        {
            if (!LoadData())
                return;

            for(int i = 0; i< data.GetLength(0);i++)
            {
                for(int p = i+1; p < data.GetLength(0); p++)
                {
                    if (data[i][0] == data[p][0] || data[i][1] != data[p][1])
                        continue;

                    DateTime firstEmpStart = StringToDate(data[i][2]);
                    DateTime secondEmpStart = StringToDate(data[p][2]);
                    DateTime firstEmpEnd = StringToDate(data[i][3]);
                    DateTime secondEmpEnd = StringToDate(data[p][3]);

                    if (secondEmpStart > firstEmpEnd || firstEmpStart > secondEmpEnd)
                        continue;

                    DateTime timeWorkedStart = (firstEmpStart > secondEmpStart ? firstEmpStart : secondEmpStart);
                    DateTime timeWorkedEnd = (firstEmpEnd < secondEmpEnd ? firstEmpEnd : secondEmpEnd);
                    double timeWorkedTogether = (timeWorkedEnd - timeWorkedStart).TotalDays;

                    string empKey;
                    if (int.Parse(data[i][0]) > int.Parse(data[p][0]))
                        empKey = data[i][0] + "," + data[p][0];
                    else
                        empKey = data[p][0] + "," + data[i][0];

                    if(!workingTimeTogether.ContainsKey(empKey))
                        workingTimeTogether.Add(empKey, Convert.ToInt32(timeWorkedTogether));
                    else
                        workingTimeTogether[empKey] += Convert.ToInt32(timeWorkedTogether);
                }
            }

            if(workingTimeTogether.Count() == 0)
            {
                Console.WriteLine("No such Employees");
                return;
            }

            string keyOfMaxValue = workingTimeTogether.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            Console.WriteLine("Employees worked together the most are: " + keyOfMaxValue + " Days: " + workingTimeTogether[keyOfMaxValue]);
        }
    }
}
