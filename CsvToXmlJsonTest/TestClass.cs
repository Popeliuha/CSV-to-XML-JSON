using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvToXmlJson;
using Xunit;

namespace CsvToXmlJsonTest
{
    public class TestClass
    {
        [Fact]
        public void FromCsv_Equals_FirstLine()
        {
            //arrange
            Converter test = new Converter();
            Hotel first=new Hotel
            {
                Capacity = 200,FoundedDate = new DateTime(2010-01-01), Name="Ukraina", Raiting = 4
            };
            //act
            List<Hotel> list = test.Read(ConfigurationManager.AppSettings["CsvPath"]);
            //assert
            Assert.Equal(list.First(),first);
        }

    }
}
