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
                            convertor.ShowDb();
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
}
