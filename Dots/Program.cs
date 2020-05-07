using System;
using System.Linq;

namespace Dots
{
    class Program
    {
        static void Main(string[] args)
        {
            Input(out int l,out int n,out int[] arr);
            

        }
        static void Input (out int l, out int n, out int[] arr)
        {
            int[] LN = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            l = LN[0];
            n = LN[1];
            arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        }
    }
}
