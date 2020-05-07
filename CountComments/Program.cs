using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CountComments
{
    class Program
    {
        static string FillInputStr() // ввести данные
        {
            string input = "";

            using (StreamReader reader = new StreamReader(@"C:\Users\Eugene\Desktop\input.txt", Encoding.ASCII)) //ввод ЗАМЕНИТЕ СТРОКУ
            {
                input = reader.ReadToEnd();
            }
            return input;
        }

        static (int a, int b, int c, int d)  ProceedSecond(string line )
        {
            (int a, int b, int c, int d) tuple = (0,0,0,0); 

            while (true) // концептуально: ищем ближайщее открытие комментария/литерала и обрезаем все до его закрытия
            {
                int[] arr = new int[4]; // получаем координаты открытий в строке
                arr[0]= line.IndexOf("(*");
                arr[1]= line.IndexOf("{");
                arr[2]= line.IndexOf("//");
                arr[3]= line.IndexOf("'");

                if ((arr[0] == arr[1] && arr[1] == arr[2] && arr[2] == arr[3] && arr[3] == -1)) 
                {
                    break; //выход из цикла если открытий не найдено
                }

                Array.Sort(arr); // сортируем массив для поиска ближайшего открытия
                
                for (int j = 0; j < arr.Length; j++)
                {
                    if (arr[j]!= -1)
                    {
                        char sublin = line[arr[j]];
                        line = line.Substring(arr[j]);
                        switch (sublin)
                        {
                            case '(':
                                line = line.Substring(line.IndexOf("*)")+2);
                                tuple.a++;
                                break;
                            case '{':
                                line = line.Substring(line.IndexOf("}")+1);
                                tuple.b++;
                                break;
                            case '/':
                                line = line.Substring(line.IndexOf("\r\n")+2);
                                tuple.c++;
                                break;
                            case '\'':
                                line = line.Substring(line.IndexOf("'")+1); //потому что открывающий и закрывающие символы равны
                                line = line.Substring(line.IndexOf("'")+1); // и сначала попадается открывающий
                                tuple.d++;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
            }
            return tuple; 
        }
        static void Main(string[] args)
        {
            string input = FillInputStr();

            (int a, int b, int c, int d) tuple = ProceedSecond(input);
            Console.WriteLine($"{tuple.a} {tuple.b} {tuple.c} {tuple.d}");
            Console.ReadKey();
        }

    }

}
