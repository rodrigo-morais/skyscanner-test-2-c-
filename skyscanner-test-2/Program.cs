using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skyscanner_test_2
{
    class Program
    {
        private static void print(List<string> managers)
        {
            Console.WriteLine(string.Join(" ", managers.ToArray()));
        }

        private static void readHierarchy(Dictionary<string, List<string>> hierarchy, int quantity)
        {
            Console.WriteLine(quantity.ToString());
            var managers = new List<string> { hierarchy.First().Key };
            while (managers.Count > 0)
            {
                print(managers);
                var lists = (from h in hierarchy where managers.Contains(h.Key) select h.Value).ToList<List<string>>();
                managers = new List<string>();
                foreach (var list in lists)
                {
                    managers = managers.Concat(list).ToList<string>();
                }
            }
        }

        private static void getHierarchy(string[] n)
        {
            var hierarchy = new Dictionary<string, List<string>>();

            for (var counter = 1; counter < n.Length; counter = counter + 1)
            {
                var employees = hierarchy.Select(x => x).Where(mngr => mngr.Key == n[counter].Split(' ')[0]).Select(ret => ret.Value).FirstOrDefault<List<string>>();
                if (employees == null)
                {
                    hierarchy.Add(n[counter].Split(' ')[0], new List<string>() { n[counter].Split(' ')[1] });
                }
                else
                {
                    employees.Add(n[counter].Split(' ')[1]);
                }
            }

            readHierarchy(hierarchy, int.Parse(n[0]));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of people:");
            var quantity = 0;
            int.TryParse(Console.ReadLine(), out quantity);

            if (quantity > 0)
            {
                var n = new string[quantity + 1];
                n[0] = quantity.ToString();

                for (var counter = 0; counter < quantity; counter = counter + 1)
                {
                    Console.WriteLine("Enter a pair manager and employee:");
                    n[counter + 1] = Console.ReadLine();
                }

                getHierarchy(n);

                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("Quantity invalid!");
            }
        }
    }
}
