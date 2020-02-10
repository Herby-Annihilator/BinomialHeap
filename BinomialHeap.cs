using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BinomialTreap.BinomialTree;

namespace BinomialTreap
{
    /// <summary>
    /// Биномиальная куча
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class BinomialHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// Количество биномиальных деревьев в куче,
        /// максимальная высота биномиильного дерева = 31,
        /// максимальное кол-во элементов в дереве = 2^30
        /// </summary>
        private const int MAX_SIZE = 31;
        /// <summary>
        /// Массив биномиальных деревьев кучи (индексы соответсвуют порядку дерева)
        /// </summary>
        private BinomialTree<T>[] binomialTreesList;

        public BinomialHeap()
        {
            binomialTreesList = new BinomialTree<T>[MAX_SIZE];
        }
        /// <summary>
        /// Добавляет элемент в кучу
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data = default(T))
        {
            // сделаем дерево из одного элемента (порядок = 0)
            BinomialTree<T> binomialTree = new BinomialTree<T>(new Node<T>(0, data)); // хз зачем мне ключ???
            if (binomialTreesList[0] == null)
            {
                binomialTreesList[0] = BinomialTree<T>.Merge(binomialTreesList[0], binomialTree);
                binomialTreesList[0].Order = 0;
            }
            else
            {
                binomialTreesList[0] = BinomialTree<T>.Merge(binomialTreesList[0], binomialTree);
                binomialTreesList[0].Order += 1;

                int currentbinomialTree = 0;
                int nexBinomialTree = 1;

                while (nexBinomialTree < MAX_SIZE)
                {
                    if (binomialTreesList[nexBinomialTree] == null)  // просто сливаем и не паримся
                    {
                        binomialTreesList[nexBinomialTree] = BinomialTree<T>.Merge(binomialTreesList[nexBinomialTree], binomialTreesList[currentbinomialTree]);
                        binomialTreesList[currentbinomialTree] = null;
                        binomialTreesList[nexBinomialTree].Order = nexBinomialTree;
                        break;
                    }
                    else    // после слияния, цикл выполнится еще раз, пока не зайдет в пункт выше
                    {
                        binomialTreesList[nexBinomialTree] = BinomialTree<T>.Merge(binomialTreesList[nexBinomialTree], binomialTreesList[currentbinomialTree]);
                        binomialTreesList[currentbinomialTree] = null;
                        binomialTreesList[nexBinomialTree].Order = nexBinomialTree + 1;
                        currentbinomialTree = nexBinomialTree;
                        nexBinomialTree++;
                    }
                }
            }
        }
        /// <summary>
        /// Проверяет кучу на пустоту
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            for (int i = 0; i < MAX_SIZE; i++)
            {
                if (binomialTreesList[i] != null)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Складывает две кучи
        /// </summary>
        /// <param name="binomialHeap"></param>
        private void Merge(BinomialHeap<T> binomialHeap)
        {

        }

        /// <summary>
        /// Печатает кучу
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < MAX_SIZE; i++)
                if (binomialTreesList[i] != null)
                {
                    Console.WriteLine("***************************");
                    binomialTreesList[i].PrintTree();
                }
                else
                {
                    Console.WriteLine("\nnull\n");
                }
        }

        /// <summary>
        /// Напечатает первые К натуральных чисел в порядке возрастания
        /// </summary>
        /// <param name="k"></param>
        public void PrintFirstNaturalElements(int k)
        {
            int howMuchPrint;
            List<T> elements = new List<T>();
            for (int i = 0; i < MAX_SIZE; i++)
                if (binomialTreesList[i] != null)
                {
                    elements.AddRange(binomialTreesList[i].GetListOfElements());
                }
            var comparer = Comparer<T>.Default;
            elements.Sort(comparer);
            if (k > elements.Count)
            {
                // IT'S VERY BAD TO DO AS ME!!!!
                Console.Write("В куче нет столько натуральных чисел. Все равно вывести? y/n");
                char symbol;
                do
                {
                    symbol = Console.ReadKey(true).KeyChar;
                } while (symbol != 'y' && symbol != 'n');
                if (symbol == 'n')
                {
                    return;
                }
                // IT'S VERY BAD TO DO AS ME!!!!
                howMuchPrint = elements.Count;
            }
            else
            {
                howMuchPrint = k;
            }
            for (int i = 0; i < howMuchPrint; i++)
            {
                Console.Write(elements[i] + " ");
            }
        }

        public void WriteBinomialHeapToFile(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName, true);
            


            for (int i = 0; i < MAX_SIZE; i++)
                if (binomialTreesList[i] != null)
                {
                    writer.WriteLine("***************************");
                    binomialTreesList[i].PrintTreeToFile(writer);
                }
                else
                {
                    writer.WriteLine("\nnull\n");
                }
            writer.Close();
        }
    }
}
