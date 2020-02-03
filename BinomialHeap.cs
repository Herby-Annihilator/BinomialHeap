using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinomialTreap.BinomialTree;

namespace BinomialTreap
{
    public class BinomialHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// Количество биномиальных деревьев в куче,
        /// максимальная высота биномиильного дерева == 31,
        /// максимальное кол-во элементов в дереве == 2^31
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

        public bool IsEmpty()
        {
            for (int i = 0; i < MAX_SIZE; i++)
            {
                if (binomialTreesList[i] != null)
                    return false;
            }
            return true;
        }
    }
}
