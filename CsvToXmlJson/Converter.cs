using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace CsvToXmlJson
{

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
                Raiting = values.Length == 4 ? Convert.ToDecimal(0.0) : Convert.ToDecimal(values[4])
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
            newHot.Raiting = Convert.ToDecimal(Console.ReadLine());
            values.Add(newHot);
        }

        public void ListToDb(List<Hotel> values)
        {
            using (var db = new HotelsDB())
            {
                foreach (var h in values)
                {
                    db.Hotels.Add(new Hotels
                    {
                        HotelId = h.Id,
                        HotelName = h.Name,
                        CreationDate = h.FoundedDate,
                        Capacity = h.Capacity,
                        Rating = decimal.Parse(h.Raiting.ToString())
                    });
                }
                db.SaveChanges();
            }
        }

        public void ShowDb()
        {
            using (var db = new HotelsDB())
            {
                foreach (var hotel in db.Hotels)
                {
                    Console.WriteLine(hotel.HotelName);
                }
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

        public void CreateJson(List<Hotel> values)
        {
            string jsonPath = ConfigurationManager.AppSettings["JsonPath"];

            string json = JsonConvert.SerializeObject(values.ToArray());
            File.WriteAllText(jsonPath, json);
        }
    }
}
