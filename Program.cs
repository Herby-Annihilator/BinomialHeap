using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BinomialTreap.Common;

namespace BinomialTreap
{
    class Program
    {
        static void Main(string[] args)
        {
            bool goOut = false;
            BinomialHeap<int> binomialHeap = null;
            do
            {
                char whatToDo;

                whatToDo = Subroutines.PrintMenu();
                switch (whatToDo)
                {
                    //
                    // c - создать кучу и заполнить ее случайными величинами
                    //
                    case 'c':
                        {
                            bool isError;
                            int number;
                            binomialHeap = new BinomialHeap<int>();
                            do
                            {

                                isError = false;
                                do
                                {
                                    Console.Write("Сколько элементов будет в куче?\nВаше число ");
                                } while (!Int32.TryParse(Console.ReadLine(), out number));
                                if (number <= 0 || number > 10000)
                                {
                                    isError = true;
                                }
                            } while (isError == true);
                            for (int i = 0; i < number; i++)
                            {
                                binomialHeap.Add(new Random().Next(-50, 1000));
                                System.Threading.Thread.Sleep(30);
                            }
                            SaverLoader.SaveToFile("input.dat", binomialHeap);
                            Console.WriteLine("Куча успешно сформирована. Нажмите что-нибудь...");
                            Console.ReadKey();
                            try
                            {
                                binomialHeap.WriteBinomialHeapToFile("output.dat");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("GG in file output.dat");
                                Console.ReadKey();
                            }
                            break;
                        }
                    //
                    //  r - добавить элементы в кучу
                    //
                    case 'r':
                        {
                            int number;
                            do
                            {
                                do
                                {
                                    Console.Write("Сколько элементов добавить? (не более 10)\nВаше число ");
                                } while (!Int32.TryParse(Console.ReadLine(), out number));
                            } while (number < 1 || number > 10);

                            if (binomialHeap == null)
                            {
                                binomialHeap = new BinomialHeap<int>();
                            }
                            for (int i = 0; i < number; i++)
                            {
                                int digit;
                                do
                                {
                                    Console.Write("\nЧисло №" + (i + 1) + " = ");
                                } while (!Int32.TryParse(Console.ReadLine(), out digit));
                                binomialHeap.Add(digit);
                            }
                            Console.WriteLine("Элементы успешно добавлены. Нажмите что-нибудь...");
                            Console.ReadKey();
                            if (File.Exists("input.dat"))
                            {
                                File.Delete("input.dat");
                            }
                            SaverLoader.SaveToFile("input.dat", binomialHeap);
                            try
                            {
                                binomialHeap.WriteBinomialHeapToFile("output.dat");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("GG in file output.dat");
                                Console.ReadKey();
                            }
                            break;
                        }
                    //
                    // b - восстановить кучу из файла input.dat
                    //
                    case 'b':
                        {
                            binomialHeap = (BinomialHeap<int>)SaverLoader.LoadFromFile("input.dat");
                            Console.WriteLine("Куча успешно восстановлена. Нажмите что-нибудь...");
                            Console.ReadKey();
                            break;
                        }
                    //
                    //  p - показать кучу
                    //
                    case 'p':
                        {
                            if (binomialHeap == null)
                            {
                                Console.WriteLine("\nКуча не существует. Нажмите что-нибудь...");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("\n---------------------------");
                            binomialHeap.Print();
                            Console.ReadKey();
                            break;
                        }
                    //
                    //  d - вывести первые К натуральных чисел
                    //
                    case 'd':
                        {
                            if (binomialHeap == null)
                            {
                                Console.WriteLine("\nКуча не существует. Нажмите что-нибудь...");
                                Console.ReadKey();
                                break;
                            }
                            int k;
                            do
                            {
                                do
                                {
                                    Console.Write("Сколько элементов вывести?\nВаше число ");
                                } while (!Int32.TryParse(Console.ReadLine(), out k));
                            } while (k < 1);
                            binomialHeap.PrintFirstNaturalElements(k);
                            Console.ReadKey();
                            break;
                        }
                    //
                    //  ESC - выход
                    //
                    case (char)27:
                        {
                            goOut = true;
                            break;
                        }
                }
            } while (goOut == false);
        }
    }
}
