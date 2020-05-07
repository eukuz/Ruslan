using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Transactions;

namespace ManyFields
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberEntries = Convert.ToInt32(Console.ReadLine()); //N
            int numberParams = Convert.ToInt32(Console.ReadLine()); //k
            int[] priorities = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            for (int i = 0; i < priorities.Length; i++) // потому что в массивах счет с 0
            {
                priorities[i] -= 1;
            }

            List<Entry> inputEntries = new List<Entry>();
            for (int i = 0; i < numberEntries; i++) // заполннение списка первичными записями 
            {
                string input = Console.ReadLine();
                string name = input.Substring(0, input.IndexOf(' '));
                string ps = input.Substring(input.IndexOf(' ') + 1);
                inputEntries.Add(new Entry
                {
                    Name = name,
                    Params = ps.Split(' ').Select(x => int.Parse(x)).ToArray()
                });
            }

            Console.WriteLine();
            ProceedEntries(ref inputEntries, priorities, 0);
        }


        static void ProceedEntries(ref List<Entry> entries, int[] priors, int p) // рекурсивная функция ищет наибольшую по приоритету запись, выводит ее затем возвращается к тому что осталось
        {
            while (entries.Count > 0)
            {
                if (entries.Count == 1)
                {
                    Console.WriteLine(entries[0].Name);
                    break;
                }

                SortDueToPriority(ref entries, priors[p]);
                int max = entries[0].Params[priors[p]];

                if (entries.Where(a => a.Params[priors[p]] == max).Count() == 1) // если записей с макс приоритетом 1 шт.
                {
                    Console.WriteLine(entries.Where(a => a.Params[priors[p]] == max).FirstOrDefault().Name);
                    entries.Remove(entries.Where(a => a.Params[priors[p]] == max).FirstOrDefault());
                }
                else
                {
                    List<Entry> selectedEntries = entries.Where(a => a.Params[priors[p]] == max).ToList();
                    if (CheckIfParamsEquivalent(selectedEntries)) // если записи равны по параметрам вывести их
                    {
                        foreach (Entry entry in selectedEntries)
                        {
                            Console.WriteLine(entry.Name);
                        }
                    }
                    else // иначе обработать выделенные записи (рекурсия)
                    {
                        ProceedEntries(ref selectedEntries, priors, p + 1);
                    }

                    int index = entries.IndexOf(entries.Where(a => a.Params[priors[p]] == max).FirstOrDefault());
                    int count = entries.Where(a => a.Params[priors[p]] == max).Count();
                    entries.RemoveRange(index, count);
                }

                if (GetNumOfDiffParamsAtPrior(entries, p) == 1) // если в оставшихся записиях значения данного приоритетного параметра равны
                {
                    p++; //переходим к следующему приоритету в заданном порядке 
                }
            }
        }

        private static bool CheckIfParamsEquivalent(List<Entry> entries) // проверить равны ли все параметры у заданных записей
        {
            int paramlenght = entries[0].Params.Length;
            for (int i = 0; i < paramlenght; i++)
            {
                for (int j = 0; j < entries.Count - 1; j++)
                {
                    if (entries[j].Params[i] != entries[j + 1].Params[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static void SortDueToPriority(ref List<Entry> entries, int priority) //отсортировать записи по убыванию относительно выбранного приоритета
        {
            Entry temp;
            for (int i = 0; i < entries.Count; i++)
            {
                for (int j = i; j < entries.Count; j++)
                {
                    if (entries[i].Params[priority] < entries[j].Params[priority])
                    {
                        temp = entries[i];
                        entries[i] = entries[j];
                        entries[j] = temp;
                    }
                }
            }
        }

        static int GetNumOfDiffParamsAtPrior(List<Entry> entries, int p) // получить число различающихся параметров на данном приоритете
        {
            List<int> diffParams = new List<int>();
            foreach (Entry entry in entries)
            {
                if (!diffParams.Contains(entry.Params[p]))
                {
                    diffParams.Add(entry.Params[p]);
                }
            }
            return diffParams.Count;
        }

    }
    class Entry// класс для упаковки записи
    {
        public string Name { get; set; }
        public int[] Params { get; set; }
    }
}
