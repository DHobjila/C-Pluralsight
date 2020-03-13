using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            Book book = null;
            IBook book = new DiskBook("Dragos's book");
            //var book = new InMemoryBook("Dragos Grade Book");
            book.GradeAdded += OnGradeAdded;

            //book.AddGrade(84.5);
            //book.AddGrade(90.5);
            //book.AddGrade(77.5);
            EnterGrades(book);
            var stats = book.GetStatistics();

            //book.Name = "";
            //Console.WriteLine(InMemoryBook.CATEGORY);
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter is {stats.Letter}");

        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Please enter a grade or 'q' to quit");
                var readedGrade = Console.ReadLine();

                if (readedGrade == "q")
                    break;

                try
                {
                    var grade = double.Parse(readedGrade);
                    book.AddGrade(grade);

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("***");
                }


            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
