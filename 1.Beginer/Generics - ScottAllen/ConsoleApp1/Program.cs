using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeDb>());

            using (IRepository<Employee> employeeRepository = new SqlRepository<Employee>(new EmployeeDb()))
            {
                AddEmployees(employeeRepository);
                AddManagers(employeeRepository);
                CountEmployees(employeeRepository);
                QueryEmployees(employeeRepository);
                DumpPeople(employeeRepository);
                

                IEnumerable<Person> temp = employeeRepository.FindAll();
            }
            
        }

        private static void AddManagers(IWriteOnlyRepository<Manager> employeeRepository)
        {
            employeeRepository.Add(new Manager { Name = "Hobjila" });
            employeeRepository.Commit();
        }

        private static void DumpPeople(IReadOnlyRepository<Person> employeeRepository)
        {
            var employees = employeeRepository.FindAll();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployees(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.FindbyId(1);
            Console.WriteLine(employee.Name);
        }

        private static void CountEmployees(IRepository<Employee> employeeRepository)
        {
            Console.WriteLine(employeeRepository.FindAll().Count());
            
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            employeeRepository.Add(new Employee { Name = "Dragos" });
            employeeRepository.Add(new Employee { Name = "Claudiu" });
            employeeRepository.Add(new Employee { Name = "Luci" });
            employeeRepository.Commit();
        }
    }
}
