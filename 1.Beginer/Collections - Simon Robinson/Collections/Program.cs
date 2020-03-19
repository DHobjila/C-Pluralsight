using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] daysOfWeek =
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };

            //Console.WriteLine("Wich day do you want to display?");
            //Console.WriteLine("Monday = 1, and so on");
            //int iDay = int.Parse(Console.ReadLine());
            //Console.WriteLine("The day is: {0}", daysOfWeek[iDay-1]);
            Console.WriteLine("Before");
            foreach(string day in daysOfWeek)
            {
                Console.WriteLine(day);
            }

            daysOfWeek[4] = "Vine weekend-ul";

            Console.WriteLine("\r\nAfter");
            foreach (string day in daysOfWeek)
            {
                Console.WriteLine(day);
            }
        }
    }
}
