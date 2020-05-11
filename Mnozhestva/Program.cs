using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //ввод чисел
            Console.Write("Введите множество чисел A и B разделённых 0 : ");
            string a = Console.ReadLine();
            var st = new System.Diagnostics.Stopwatch();
            st.Start();
            String[] w = a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//разбитие строки
            int l = w.Length - 2;//длина строки
            int[] nums = new int[l];//Создание массива
            int[] nums1 = new int[l];//Создание массива
            int p = 0;
            for (int i = 0; ; i++)//Бесконечный цикл
            {
                if (Convert.ToInt32(w[i]) == 0) break;//выход из цикла при 0
                nums[i] = Convert.ToInt32(w[i]);// Заполнение массива
                p = i;
            }
            p += 2;
            for (int j = 0; ; j++)//Бесконечный цикл
            {
                if (Convert.ToInt32(w[p]) == 0) break;//выход из цикла при 0
                nums1[j] = Convert.ToInt32(w[p]);// Заполнение массива
                p++;
            }
            bool found = false;
            for (int j = 0; j <= (l) - 1; j++)//Бесконечный цикл
            {
                found = false;
                for (int i = 0; i <= (l) - 1; i++)//Бесконечный цикл
                {
                    if (nums[j] == nums1[i])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) Console.Write(" " + nums[j]);
            }
            for (int j = 0; j <= (l) - 1; j++)//Бесконечный цикл
            {
                found = false;
                for (int i = 0; i <= (l) - 1; i++)//Бесконечный цикл
                {
                    if (nums[i] == nums1[j])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) Console.Write(" " + nums1[j]);
            }
            st.Stop(); //остановить
            Console.WriteLine();
            Console.WriteLine(st.Elapsed);
            Console.ReadKey(true);
        }
    }
}