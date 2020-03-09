using System;
using System.Collections.Generic;
using System.IO;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var adress = @"data.csv";
            var resultAdress = @"result.xml";
            var resultType = "xml";
            var logAdress = "log.txt";
            var today = DateTime.UtcNow;

            var lines = File.ReadLines(adress);
            ICollection<Student> students = new HashSet<Student>(new OwnComparer());

            using (var log = new StreamWriter(logAdress))
            {
                foreach (var line in lines)
                {
                    var data = line.Split(",");
                    bool correct = true;
                    foreach (var item in data)
                    {
                        if (string.IsNullOrEmpty(item))
                        {
                            correct = false;
                        }
                    }
                    if (correct && data.Length < 9)
                    {
                        log.WriteLine(line);
                    } else
                    {
                        var studies = new Studies { StudiesName = data[2], StudiesMode = data[3] };
                        var student = new Student
                        {
                            FirstName = data[0],
                            SecondtName = data[1],
                            Studies = studies,
                            IndexNumber = "s"+data[4],
                            Birthdate = DateTime.Parse(data[5]),
                            Email = data[6],
                            MotherName = data[7],
                            FathersName = data[8],
                        };
                        students.Add(student);
                    }
                }
            }
        }
    }
}
