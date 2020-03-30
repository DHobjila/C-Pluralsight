using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterOrdeProjecting
{
    class Program
    {

        static void Main(string[] args)
        {
            var cars = ProcessCar("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");


            Console.WriteLine("================== Different Queries on car processing ===================");

            var query = cars.OrderByDescending(c => c.Combined)
                            .ThenBy(c => c.Name);

            var querySyntax =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name ascending
                select new
                {
                    car.Manufacturer,
                    car.Name,
                    car.Combined
                };
            var result = cars.Select(c => new { c.Manufacturer, c.Name, c.Combined });


            var queryExtensionMethods =
                                cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                                    .OrderByDescending(c => c.Combined)
                                    .ThenBy(c => c.Name);
            //.First()
            //.Last()

            var ask = cars.Any(c => c.Manufacturer == "Ford");
            Console.WriteLine(ask);

            var anon = new // obiect anonim
            {
                Name = "Dragos"
            };

            //foreach (var car in queryExtensionMethods.Take(10))
            //{
            //    Console.WriteLine($"{car.Manufacturer}: {car.Name}: {car.Combined}");
            //}


            // SELECT si SELECT MANY

            //var result1 = cars.Select(c => c.Name);
            //foreach (var name in result1)
            //{
            //    foreach (var character in name)
            //    {
            //        Console.WriteLine(character);
            //    }
            //}

            //var result2 = cars.SelectMany(c => c.Name)
            //                  .OrderBy(c => c);
            //foreach (var item in result2)
            //{
            //    Console.WriteLine(item);
            //}

            Console.WriteLine("======================JOIN=============================");
            
            var querySyntaxJoin =
                from car in cars
                join manufacturer in manufacturers on car.Manufacturer equals manufacturer.Name
                orderby car.Combined descending, car.Name ascending
                select new
                {
                    manufacturer.Headquarter,
                    car.Name,
                    car.Combined
                };

            var querySyntaxJoinComposition =
                from car in cars
                join manufacturer in manufacturers 
                on 
                new { car.Manufacturer, car.Year } 
                equals 
                new { Manufacturer = manufacturer.Name, manufacturer.Year }
                orderby car.Combined descending, car.Name ascending
                select new
                {
                    manufacturer.Headquarter,
                    car.Name,
                    car.Combined
                };


            foreach (var car in querySyntaxJoinComposition.Take(10))
            {
                Console.WriteLine($"{car.Headquarter}: {car.Name}: {car.Combined}");
            }


            var queryExtensionMethod =
                cars.Join(manufacturers, c => c.Manufacturer,
                                         m => m.Name, 
                                         (c, m) => new
                                         {
                                             m.Headquarter,
                                             c.Name,
                                             c.Combined
                                         })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            var queryExtensionMethodComposition =
                cars.Join(manufacturers, c => new { c.Manufacturer, c.Year },
                                         m => new { Manufacturer = m.Name, m.Year },
                                         (c, m) => new
                                         {
                                             m.Headquarter,
                                             c.Name,
                                             c.Combined
                                         })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            foreach (var car in queryExtensionMethod.Take(10))
            {
                Console.WriteLine($"{car.Headquarter}: {car.Name}: {car.Combined}");
            }

            Console.WriteLine("============================Grouping Data================================");
           

            var querySintax1 =
                from car in cars
                group car by car.Manufacturer.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            var queryExtMeth =
                cars.GroupBy(c => c.Manufacturer.ToUpper())
                    .OrderBy(g => g.Key);

            foreach (var group in queryExtMeth)
            {
                Console.WriteLine(group.Key);
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            Console.WriteLine("====================== Group Join ==============================");

            var querySintax2 =
                from manufacturer in manufacturers
                join car in cars on manufacturer.Name equals car.Manufacturer
                    into carGroup
                orderby manufacturer.Name
                select new
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup
                };

            var extensionMethod =
                manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer, 
                               (m, g) =>
                                        new
                                        {
                                            Manufacturer = m,
                                            Cars = g
                                        })
                .OrderBy(m => m.Manufacturer.Name);

            var exercise =
                manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
                               (m, g) =>
                                        new
                                        {
                                            Manufacturer = m,
                                            Cars = g
                                        })
                              .GroupBy(m => m.Manufacturer.Headquarter);

            foreach (var group in exercise)
            {
                Console.WriteLine($"{group.Key}");
                foreach (var car in group.SelectMany(g => g.Cars)
                                         .OrderByDescending(c => c.Combined).Take(3))
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }

            //foreach (var group in extensionMethod)
            //{
            //    Console.WriteLine($"{group.Manufacturer.Name} : {group.Manufacturer.Headquarter}");
            //    foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} : {car.Combined}");
            //    }
            //}
            Console.WriteLine("===============---- AGREGATING ---=====================");

            var querySynth =
                from car in cars
                group car by car.Manufacturer into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into resulti
                orderby resulti.Max descending
                select resulti;

            var extensionMethod2 =
                cars.GroupBy(c => c.Manufacturer)
                    .Select(g =>

                    {
                        var results = g.Aggregate(new CarStatistics(), (acc, c) => acc.Accumulate(c),
                                                                        acc => acc.Compute());
                        return new
                        {
                            Name = g.Key,
                            Avg = results.Average,
                            Min = results.Min,
                            Max = results.Max,
                        };
                    })
                    .OrderByDescending(r => r.Max);


            foreach (var result1 in extensionMethod2)
            {
                Console.WriteLine($"{result1.Name}");
                Console.WriteLine($"Max: {result1.Max}");
                Console.WriteLine($"Min: {result1.Min}");
                Console.WriteLine($"Average: {result1.Avg}");


            }
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
