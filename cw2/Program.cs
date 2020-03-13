using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            var adress = args.Length > 1 ? args[0] : @"dane.csv";
            var resultAdress = args.Length > 2 ? args[1] : @"result.xml";
            var resultType = args.Length > 3 ? args[2] : "xml";
            var logAdress = "log.txt";
            var today = DateTime.Now.ToString("dd.MM.yyyy");

            try
            {
                var lines = File.ReadLines(adress);
                var students = new HashSet<Student>(new OwnComparer());
                Dictionary<string, int> activeStdCount = new Dictionary<string, int>();

                using (var log = new StreamWriter(logAdress))
                {
                    foreach (var line in lines)
                    {
                        var data = line.Split(",");
                        bool correct = true;
                        foreach (var item in data)
                        {
                            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                            {
                                correct = false;
                            }
                        }
                        if (!correct)
                        {
                            log.WriteLine("Puste dane " + line);
                        }
                        else if (correct && data.Length < 9)
                        {
                            log.WriteLine("Dane za krótkie " + line);
                        } else
                        {
                            if (!activeStdCount.ContainsKey(data[2]))
                            {
                                activeStdCount.Add(data[2], 1);
                            } else
                            {
                                activeStdCount[data[2]]++;
                            }

                            var studies = new Studies { StudiesName = data[2], StudiesMode = data[3] };
                            var student = new Student
                            {
                                FirstName = data[0],
                                SecondtName = data[1],
                                Studies = studies,
                                IndexNumber = "s" + data[4],
                                Birthdate = DateTime.Parse(data[5]),
                                Email = data[6],
                                MotherName = data[7],
                                FathersName = data[8],
                            };
                            if (!students.Add(student))
                            {
                                log.WriteLine("Student się powtarza " + line);
                            }
                        }
                    }
                }

                var activeStd = new List<ActiveStudies>();
                foreach (var item in activeStdCount)
                {
                    activeStd.Add(new ActiveStudies { id = item.Key, value = item.Value });
                }

                var uczelnia = new Uczelnia
                {
                    createdAt = today,
                    studenci = students,
                    activeStudies = activeStdCount
                };

                if (resultType == "xml")
                {
                    XDocument doc = new XDocument(new XElement("university",
                            new XAttribute("createdAt", today),
                            new XAttribute("author", "Piotr Dębowski"),
                            new XElement("studenci",
                                from student in students
                                select new XElement("student",
                                    new XAttribute("indexNumber", student.IndexNumber),
                                    new XElement("name", student.FirstName),
                                    new XElement("secondName", student.SecondtName),
                                    new XElement("birthdate", student.Birthdate),
                                    new XElement("email", student.Email),
                                    new XElement("mothersName", student.MotherName),
                                    new XElement("fathersName", student.FathersName),
                                    new XElement("studies",
                                        new XElement("name", student.Studies.StudiesName),
                                        new XElement("mode", student.Studies.StudiesMode)
                                    ))),
                            new XElement("activeStudies",
                                from asc in activeStdCount
                                select new XElement("studies",
                                    new XAttribute("name", asc.Key),
                                    new XAttribute("numberOfStudents", asc.Value)
                                )
                            )));
                    doc.Save(resultAdress);

                    //var ns = new XmlSerializerNamespaces();
                    //ns.Add("", "");
                    //FileStream writer = new FileStream(resultAdress, FileMode.Create);
                    //XmlSerializer serializer = new XmlSerializer(typeof(Uczelnia), new XmlRootAttribute("uczelnia"));
                    //serializer.Serialize(writer, uczelnia, ns);
                } else if (resultType == "json")
                {
                    var json = JsonConvert.SerializeObject(uczelnia, (Newtonsoft.Json.Formatting)Formatting.Indented);
                    File.WriteAllText(resultAdress + ".json", json);
                }
            } catch (FileNotFoundException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            } catch (ArgumentException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            } catch (IOException e)
            {
                Console.WriteLine("Exception caught: {0}", e);
            }
        }
    }
}
