using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Employee
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write absolute path to txt file.");
            string testFileName = Console.ReadLine();
            if(testFileName == null || !File.Exists(testFileName))
            {
                Console.WriteLine("File Not Found.");
                return;
            }
            string[][] data = File.ReadAllLines(testFileName).Select(x => x.Split(',')).ToArray();
            Dictionary<string, int> workingTimeTogether = new Dictionary<string, int>();
            for(int i = 0; i< data.GetLength(0);i++)
            {
                for(int p = i+1; p < data.GetLength(0); p++)
                {
                    if (data[i][0] == data[p][0] || data[i][1] != data[p][1])
                        continue;
                    DateTime firstEmpStart = Convert.ToDateTime(data[i][2]);
                    DateTime secondEmpStart = Convert.ToDateTime(data[p][2]);
                    DateTime firstEmpEnd;
                    DateTime secondEmpEnd;
                    if (data[i][3] == " Null")
                    {
                        firstEmpEnd = DateTime.Now;
                    }
                    else
                    {
                        firstEmpEnd = Convert.ToDateTime(data[i][3]);
                    }

                    if(data[p][3] == " Null")
                    {
                        secondEmpEnd = DateTime.Now;
                    }
                    else
                    {
                        secondEmpEnd = Convert.ToDateTime(data[p][3]);
                    }

                    double timeWorkedTogether = (firstEmpStart - secondEmpStart).TotalDays;
                    double temp = (firstEmpStart - secondEmpStart).TotalDays;
                    double temp2 = (firstEmpEnd - secondEmpEnd).TotalDays;

                    if (timeWorkedTogether <= 0)
                        continue;

                    if ( temp > 0 )
                    {
                        timeWorkedTogether -= temp;
                    }

                    if(temp2 > 0 )
                    {
                        timeWorkedTogether -= temp2;
                    }

                    string empKey = data[i][0] + "," + data[p][0];
                    if(!workingTimeTogether.ContainsKey(empKey))
                    {
                        workingTimeTogether.Add(empKey, Convert.ToInt32(timeWorkedTogether));
                    }
                    else
                    {
                        workingTimeTogether[empKey] += Convert.ToInt32(timeWorkedTogether);
                    }
                }
            }

            if(workingTimeTogether.Count() == 0)
            {
                Console.WriteLine("No such Employees");
                return;
            }

            string keyOfMaxValue = workingTimeTogether.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
            string[] id = keyOfMaxValue.Split(",");
            Console.WriteLine("Employees worked together the most are: " + id[0] + " " + id[1]);
        }
    }
}
