using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tips
{
    class GenericsStatics
    {
        static void Main(string[] args)
        {
            var a = new Item1<int>();
            var b = new Item1<int>();
            var c = new Item1<string>();

            Console.WriteLine(Item1<int>.InstanceCount);
            Console.WriteLine(Item1<string>.InstanceCount);

        }
    }

    public class Item1<T>
    {
        public Item1()
        {
            InstanceCount += 1;
        }
        public static int InstanceCount;
    }
}
