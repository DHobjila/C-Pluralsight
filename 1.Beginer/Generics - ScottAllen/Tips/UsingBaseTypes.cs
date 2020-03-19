using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tips
{
    class UsingBaseTypes
    {
        static void Main(string[] args)
        {
            var list = new List<Item>();

            list.Add(new Item<int>());
            list.Add(new Item<double>());
        }
    }

    public class Item<T>:Item
    {
    }

    public class Item
    {

    }
}
