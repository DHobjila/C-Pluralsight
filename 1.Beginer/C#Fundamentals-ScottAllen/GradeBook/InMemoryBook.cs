using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook
{

    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
        
    }

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
          
            grades = new List<double>();
            Name = name;
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                default:
                    AddGrade(0);
                    break;

            }

        }
        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }

            }
            else
                throw new ArgumentException($"Invalid {nameof(grade)}");

        }

        public override event GradeAddedDelegate GradeAdded;

        public  override Statistics GetStatistics()
        {
            var result = new Statistics();


            //foreach (var grade in grades)
            //{
            //    //if (grade > highGrade)
            //    //{
            //    //    highGrade = grade;
            //    //}

            //    result.High = Math.Max(grade, result.High);
            //    result.Low = Math.Min(grade, result.Low);
            //    result.Average += grade;
            //}

            /// Varianta cu FOR loop
            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);                
              
            }          

            return result;
        }

        private List<double> grades;
        
        private string name;

        readonly string category = "Science"; //poate fi modificat doar in constructor
        public const string CATEGORY = "Science"; // poate fi doar citit, nu poate fi modificat 
    }
}
