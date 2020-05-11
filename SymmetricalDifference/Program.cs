using System;
using System.Collections.Generic;
using System.Linq;

namespace SymmetricalDifference
{
    class Program
    {
        static void Main(string[] args)
        {
            //ввод
            string input = Console.ReadLine();
            int[] A = input.Substring(0, input.IndexOf(" 0 ")).Split(' ').Select(x => int.Parse(x)).ToArray();
            int[] B = input.Substring(input.IndexOf(" 0 ") + 3, input.LastIndexOf(" 0 ")).Split(' ').Select(x => int.Parse(x)).ToArray();

            //обработка
            List<int> output = new List<int>();
            output.AddRange(A.Where(a => !B.Contains(a)));
            output.AddRange(B.Where(b => !A.Contains(b)));

            //вывод
            if (output.Count == 0)
                Console.WriteLine(0);
            else
            {
                output.Sort();
                foreach (var item in output)
                    Console.Write(item + " ");
            }
            
        }
    }
}
