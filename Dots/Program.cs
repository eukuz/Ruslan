using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Dots
{
    class Program
    {
        static void Main(string[] args)
        {
            Input(out int l, out int n, out List<(int, int)> dots); // Item1 - координата, Item2 - кол-во точек в пределах доступа

            int finalCount = 0;
            while (dots.Count>0)
            {
                SortItems2Descending(ref dots);
                (int, int) dot = dots[0];

                SortItems1Ascending(ref dots);
                dots.RemoveRange(dots.IndexOf(dot), dot.Item2);

                SetNumsOfNearDots(ref dots, l);
                finalCount++;
            }

            Console.WriteLine(finalCount);
        }

        static void SortItems1Ascending(ref List<(int, int)> dots) // отсортировать по возрастанию относительно расположения на прямой
        {
            (int, int) temp;
            for (int i = 0; i < dots.Count; i++)
            {
                for (int j = i; j < dots.Count; j++)
                {
                    if (dots[i].Item1 > dots[j].Item1)
                    {
                        temp = dots[i];
                        dots[i] = dots[j];
                        dots[j] = temp;
                    }
                }
            }
        }
        static void SortItems2Descending(ref List<(int,int)> dots) // отсортировать по убыванию относительно количества ближайших точек
        {
            (int, int) temp;
            for (int i = 0; i < dots.Count; i++)
            {
                for (int j = i; j < dots.Count; j++)
                {
                    if (dots[i].Item2 < dots[j].Item2)
                    {
                        temp = dots[i];
                        dots[i] = dots[j];
                        dots[j] = temp;
                    }
                }
            }
        }
        static void Input(out int l, out int n, out List<(int, int)> dots) //обработать ввод
        {
            int[] LN = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            l = LN[0]*2;
            n = LN[1];

            int[] coordinates = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            Array.Sort(coordinates);

            dots = new List<(int, int)>();

            for (int i = 0; i < coordinates.Length; i++)
            {
                (int, int) dot = (coordinates[i], 0);
                dots.Add(dot);
            }

            SetNumsOfNearDots(ref dots, l);
        }
        static void SetNumsOfNearDots(ref List<(int,int)> dots, int l) //установить количество точек в зоне доступа
        {
            for (int i = 0; i < dots.Count; i++)
            {
                int numOfNearDots = 0;
                int coordinate = dots[i].Item1;

                for (int j = i; j < dots.Count; j++)
                {
                    if (dots[j].Item1 <= coordinate + l)
                    {
                        numOfNearDots++;
                        continue;
                    }
                    break;
                }

                dots[i] = (coordinate, numOfNearDots);
            }
        } 
    }

}
