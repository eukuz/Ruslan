using System;
using System.Data;
using System.Linq;
using System.Transactions;

namespace ManyFields
{
    class Program
    {
        static Entry[] outEntries;
        static int outIndex = 0;
        static void Main(string[] args)
        {
            int numberEntries = Convert.ToInt32(Console.ReadLine()); //N
            int numberParams = Convert.ToInt32(Console.ReadLine()); //k
            int[] priorities = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            Entry[] inputEntries = new Entry[numberEntries];
            for (int i = 0; i < numberEntries; i++)
            {
                string input = Console.ReadLine();
                string name = input.Substring(0, input.IndexOf(' '));
                string ps = input.Substring(input.IndexOf(' ') + 1);
                inputEntries[i] = new Entry
                {
                    Name = name,
                    Params = ps.Split(' ').Select(x => int.Parse(x)).ToArray()
                };
            } // заполнить массив
            Array.Sort
            outEntries = new Entry[inputEntries.Length];

            Console.WriteLine();

        }

        static void BiggerSort(ref Entry[] entries, int[] priors)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                for (int j = 0; j < priors.Length; j++)
                {
                    int max = entries[0].Params[priors[j]];
                    foreach (Entry entry in entries) //найти максимальный член данного проритета
                    {
                        if (entry.Params[priors[j]] > max)
                        {
                            max = entry.Params[priors[j]];
                        }
                    }


                }
            }
        }
        static void SortArray(ref Entry[] entries, int[] priors)
        {
            Entry[] innerEntries = new Entry[entries.Length];
            int index = 0;
            int prior = 0;

            for (int i = 0; i < entries.Length; i++)
            {


                Entry[] selectedEntries = entries.Where(a => a.Params[priors[prior]] == max).ToArray();
                for (int j = 0; j < priors.Length; j++)
                {
                    int max = entries[0].Params[priors[prior]];
                    foreach (Entry entry in entries) //найти максимальный член данного проритета
                    {
                        if (entry.Params[priors[prior]] > max)
                        {
                            max = entry.Params[priors[prior]];
                        }
                    }

                    selectedEntries = selectedEntries.Where(a => a.Params[priors[j]] == max).ToArray();
                    if (selectedEntries.Length == 1)
                    {
                        innerEntries[++index] = selectedEntries[0];
                        break;
                    }
                }
            }
        }
        static Entry[] GetHigestEntry( Entry[] entries, int prior, int[] priors)
        {
            int max = entries[0].Params[prior];
            foreach (Entry entry in entries)
            {
                if (entry.Params[prior] > max)
                {
                    max = entry.Params[prior];
                }
            }

            int numOfMaxPriorEntries = 0;
            foreach (Entry e in entries)
            {
                if (e.Params[prior] == max)
                {
                    numOfMaxPriorEntries++;
                }
            }

            if (numOfMaxPriorEntries == 1)
            {
                outEntries[outIndex++] = entries.Where(a => a.Params[prior] == max).FirstOrDefault();
                return null;
            }
            else
            {
                Entry[] selectedEntries = entries.Where(a => a.Params[prior] == max).ToArray();
                GetHigestEntry(selectedEntries, ++prior, priors);
                return selectedEntries;
            }
        }
    }
    class Entry
    {
        public string Name { get; set; }
        public int[] Params { get; set; }
    }
}
