using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvToXmlJson
{
    public class Hotel : IComparable<Hotel>
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime FoundedDate { get; set; }
        public int Capacity { get; set; }
        public decimal Raiting { get; set; }

        public int CompareTo(Hotel obj)
        {
            return obj.Raiting.CompareTo(Raiting);
        }
    }
}
