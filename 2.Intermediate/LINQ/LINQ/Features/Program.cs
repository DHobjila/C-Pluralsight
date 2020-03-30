using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int, int> square = x => x*x;
            Func<int, int, int> add = (x, y) => x + y;
            Action<int> write = x => Console.WriteLine(x);

            Console.WriteLine(square(add(3, 5)));
            write(square(add(3, 5)));

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee{Id = 2, Name = "Lucian"},
                new Employee{Id = 1, Name = "Dragos"}
                
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee{Id = 3, Name = "Claudiu"}
            };

            var query = developers.Where(e => e.Name.Length == 6)
                                             .OrderBy(e => e.Name);

            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;

            foreach (var person in query)
            {
                Console.WriteLine(person.Name);
            }

            Console.WriteLine(sales.Count());
            IEnumerator<Employee> enumerator = sales.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name);
            }
        }

    }
}
