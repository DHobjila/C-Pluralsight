using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQtoXML
{
    class Program
    {

        static void Main(string[] args)
        {
            CreateXml();
            QueryXml();
        }

        private static void QueryXml()
        {
            var ns = (XNamespace)"http//pluralsight.com/cars/2020";
            var ex = (XNamespace)"http//pluralsight.com/cars/2020/ex";

            var document = XDocument.Load("fuel.xml");
            var query =
                from element in document.Element(ns + "Cars").Elements(ex + "Car")
                where element.Attribute("Manufacturer").Value == "BMW"
                select element.Attribute("Name").Value;

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
        }

        private static void CreateXml()
        {

            var records = ProcessCar("fuel.csv");

            var ns = (XNamespace)"http//pluralsight.com/cars/2020";
            var ex = (XNamespace)"http//pluralsight.com/cars/2020/ex";


            var document = new XDocument();
            var cars = new XElement(ns + "Cars",
                        from record in records
                        select new XElement(ex + "Car",
                                                new XAttribute("Name", record.Name),
                                                new XAttribute("Combined", record.Combined),
                                                new XAttribute("Manufacturer", record.Manufacturer)
                                                ));
            cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));


            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Car> ProcessCar(string path)
        {
            return
                    File.ReadAllLines(path)
                        .Skip(1)
                        .Where(line => line.Length > 0)
                        .Select(Car.ParseFromCsv)
                        .ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarter = columns[1],
                            Year = int.Parse(columns[2])
                        };

                    });
            return query.ToList();
        }
    }

    public class CarStatistics
    {
        public CarStatistics()
        {
            Max = Int32.MinValue;
            Min = Int32.MaxValue;

        }

        public CarStatistics Accumulate(Car car)
        {
            Count += 1;
            Total += car.Combined;
            Max = Math.Max(Max, car.Combined);
            Min = Math.Min(Min, car.Combined);

            return this;
        }

        public CarStatistics Compute()
        {
            Average = Total / Count;
            return this;
        }

        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }

    }
}
