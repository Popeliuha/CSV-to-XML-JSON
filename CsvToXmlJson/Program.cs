using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                    Console.WriteLine("If you want to convert list to DB, press 4.");
                    Console.WriteLine("If you want to add new hotel to list, press 5.");
                    Console.WriteLine("If you want to convert Json to list, press 6.");
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
                        case "4":
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("To see result refresh your hotels table in sql server.");
                            Console.ResetColor();
                            convertor.ListToDb(values);
                            break;
                        case "5":
                            convertor.AddToList(values);
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("To see result press 3.");
                            Console.ResetColor();
                            break;
                        case "6":
                            List<Hotel> newList = convertor.FromJson(ConfigurationManager.AppSettings["JsonPath"]);
                            foreach (var V in newList)
                            {
                                Console.WriteLine(V.Name);
                            }
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

        public List<Hotels> ReadHotels(string addres)
        {
            List<Hotels> valuess = File.ReadAllLines(addres).Select(FromCsvHotels).ToList();
            valuess.Sort();
            return valuess;
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

        public Hotels FromCsvHotels(string csvLine)
        {
            string[] valuess = csvLine.Split(';');
            return new Hotels
            {
                HotelName = valuess[0],
                HotelId = Convert.ToInt32(valuess[1]),
                CreationDate = Convert.ToDateTime(valuess[2]),
                Capacity = Convert.ToInt32(valuess[3]),
                Rating = valuess.Length == 4 ? Convert.ToDecimal(0) : Convert.ToDecimal(valuess[4])
            };
        }
        public List<Hotel> FromJson(string adress)
        {
            var json = File.ReadAllText(adress);
            var newList = JsonConvert.DeserializeObject<List<Hotel>>(json);
            return newList;
        }


        public void AddToList(List<Hotel> values)
        {
            Hotel newHot = new Hotel();
            Console.WriteLine("Enter name:");
            newHot.Name = Console.ReadLine();
            Console.WriteLine("Enter founded date (yyyy,mm,dd)");
            newHot.FoundedDate = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter hotel capacity:");
            newHot.Capacity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter rating:");
            newHot.Raiting = Convert.ToDouble(Console.ReadLine());
            values.Add(newHot);
        }

        public void ListToDb(List<Hotel> values)
        {

            var valuess = ReadHotels(ConfigurationManager.AppSettings["CsvPath"]);
            using (var db = new HotelsDB())
            {
                foreach (var h in valuess)
                {
                    db.Hotels.Add(h);
                }
                /*var hot = new Hotels
                {
                    HotelName = "Umnichka",
                    CreationDate = new DateTime(1998, 01, 01),
                    Capacity = 400,
                    Rating = 2
                };
                db.Hotels.Add(hot);*/
                db.SaveChanges();
            }
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
