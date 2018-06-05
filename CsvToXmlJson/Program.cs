using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace CsvToXmlJson
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Converter convertor = new Converter();
                var values = convertor.Read(ConfigurationManager.AppSettings["CsvPath"]);

                bool flag = true;
                while (flag)
                {
                    Console.WriteLine("If you want to convert to XML, press 1.");
                    Console.WriteLine("If you want to convert to JSON, press 2.");
                    Console.WriteLine("If you want to see LIST, press 3.");
                    Console.WriteLine("Press any other key to EXIT.");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(new string('=', 120));
                    Console.ResetColor();
                    string result = Console.ReadLine();
                    switch (result)
                    {
                        case "1":
                            convertor.CreateXml(values);
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("You can find XML in CsvToXmlJson/bin/Debug folder");
                            Console.ResetColor();
                            break;
                        case "2":
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("You can find JSON in CsvToXmlJson/bin/Debug folder");
                            Console.WriteLine("Pls enter PERENOS PO SLOVAM in your notepad :D");
                            Console.ResetColor();
                            convertor.CreateJson(values);
                            break;
                        case "3":
                            foreach (var v in values)
                            {
                                Console.WriteLine(v.Id + "    " + v.Name + "  " + v.FoundedDate.Date + "   " + v.Raiting + "   " + v.Capacity);
                            }
                            Console.WriteLine(new string('-', 120));
                            break;
                        default:
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("HAVE A NICE DAY MAN");
                            flag = false;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
            Console.ReadKey();
        }
    }

    public class Converter
    {
        public List<Hotel> Read(string addres)
        {
            List<Hotel> values = File.ReadAllLines(addres).Select(FromCsv).ToList();
            values.Sort();
            return values;
        }

        public Hotel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            return new Hotel
            {
                Name = values[0],
                Id = Convert.ToInt32(values[1]),
                FoundedDate = Convert.ToDateTime(values[2]),
                Capacity = Convert.ToInt32(values[3]),
                Raiting = values.Length == 4 ? Convert.ToDouble(0) : Convert.ToDouble(values[4])
            };
        }

        public void CreateXml(List<Hotel> values)
        {
            string xmlPath = ConfigurationManager.AppSettings["XmlPath"];

            var xmlWriter = new XmlTextWriter("hotels.xml", null);
            xmlWriter.WriteStartDocument();

            xmlWriter.Close();
            var hotelsXml = values.Select(i => new XElement("Hotel",
                new XElement("Id", i.Id),
                new XElement("Name", i.Name),
                new XElement("Date", i.FoundedDate.Date),
                new XElement("Capasity", i.Capacity),
                new XElement("Raiting", i.Raiting)));
            var bodyXml = new XElement("Hotel", hotelsXml);
            bodyXml.Save(xmlPath);
        }

        public  void CreateJson(List<Hotel> values)
        {
            string jsonPath = ConfigurationManager.AppSettings["JsonPath"];

            string json = JsonConvert.SerializeObject(values.ToArray());
            File.WriteAllText(jsonPath, json);
        }
    }
    public class Hotel : IComparable<Hotel>
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime FoundedDate { get; set; }
        public int Capacity { get; set; }
        public double Raiting { get; set; }

        public int CompareTo(Hotel obj)
        {
            return obj.Raiting.CompareTo(Raiting);
        }
    }
}
