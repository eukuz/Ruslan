using System;
using System.IO;
using System.Linq;

namespace FileSort
{
    class Program
    {
        static string inPath = @"C:\Users\Eugene\Desktop\input.txt";
        static string outPath = @"C:\Users\Eugene\Desktop\output.txt";
        static void Main(string[] args)
        {
            File.WriteAllText(outPath, String.Empty); // clear Outputfile
            int[] arrIds = new int[getNumberOfStrings()];
            FillArray(ref arrIds);
            Sort(ref arrIds);
            OutputArray(arrIds);
            Console.WriteLine();
        }
        static void OutputArray(int[] arr) //вывести строки, построчно подгружая их из файла ввода
        {
            for (int i = 0; i < arr.Length; i++)
            {
                WriteLine(GetLine(arr[i]));
            }
        }
        static void FillArray(ref int[] arr) // заполняем массив -1 чтобы не путать с адресами
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = -1;
            }
        }
        static int getNumberOfStrings() //узнаем сколько строк в файле
        {
            int i = 0;
            using (StreamReader reader = new StreamReader(inPath))
            {
                while (reader.ReadLine() != null)
                {
                    i++;
                }
            }
            return i;
        }

        static void Sort(ref int[] arr) //заполняем массив отсортированными адресами строк
        {
            string minLine = "";
            for (int i = 0; i < arr.Length; i++)
            {
                for (int k = 0; k < arr.Length; k++) // взять для сравнения строку, которой еще нет в массиве
                {
                    if (!arr.Contains(k))
                    {
                        minLine = GetLine(k);
                        break;
                    }
                }

                for (int j = 0; j < arr.Length; j++) // найти наименьшую строку (в алф. порядке) и записать в массив
                {
                    if (!arr.Contains(j)) // пропускать "отсортированные" строки
                    {
                        if (String.Compare(minLine, GetLine(j)) >= 0)
                        {
                            minLine = GetLine(j);
                            arr[i] = j;
                        }
                    }
                }
            }
        }
        static string GetLine(int id)
        {
            return File.ReadLines(inPath).Skip(id).Take(1).First(); //получить n-ю строку из файла 
        }
        static void WriteLine(string line) //записываем строку в файл
        {
            using (StreamWriter sw = new StreamWriter(outPath, true))
            {
                sw.WriteLine(line);
            }
        }
    }
}
