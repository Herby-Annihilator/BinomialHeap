﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinomialTreap.BinomialTree
{
    /// <summary>
    /// Биномиальное дерево
    /// </summary>
    public class BinomialTree<T> where T : IComparable<T>
    {


        private Node<T> root;
        /// <summary>
        /// Порядок дерева (или его высота), если дерво пустое, то порядок == -1
        /// </summary>
        public int Order { get; set; }

        public BinomialTree()
        {
            Order = -1;
            root = null;
        }

        public BinomialTree(Node<T> root)
        {
            Order = 0;
            this.root = root;
        }
        /// <summary>
        /// Выполняет слияние двух деревьев. В результате - новое биномиальное дерево
        /// Сливаются деревья одинакового порядка.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static BinomialTree<T> Merge(BinomialTree<T> first, BinomialTree<T> second)
        {
            if (first == null)
                return second;
            else if (second == null)
                return first;

            var comparer = Comparer<T>.Default;
            //
            // если не сработает, то нужно сравнивать по ключу
            //
            if (first.Order == 0 && second.Order == 0)
            {
                if (comparer.Compare(first.root.Data, second.root.Data) < 0)
                {
                    first.root.LeftChild = second.root;
                    second.root.Parent = first.root;
                    return new BinomialTree<T>(first.root);
                }
                else
                {
                    second.root.LeftChild = first.root;
                    first.root.Parent = second.root;
                    return new BinomialTree<T>(second.root);
                }
            }
            else
            {
                if (comparer.Compare(first.root.Data, second.root.Data) < 0)  // first < second
                {
                    HookUpTrees(first, second);
                    return new BinomialTree<T>(first.root);
                }
                else
                {
                    HookUpTrees(second, first);
                    return new BinomialTree<T>(second.root);
                }
            }          
        }
        /// <summary>
        /// Сцепляет два дерева, корень первого меньше корня второго.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static void /*BinomialTree<T>*/ HookUpTrees(BinomialTree<T> first, BinomialTree<T> second)
        {
            Node<T> lastRightBrother, shouldBecomeRightBrother;

            second.root.Parent = first.root;

            // сцепка деревьев
            lastRightBrother = first.root.LeftChild;
            shouldBecomeRightBrother = second.root;
            do
            {
                // Дойти до самого крайнего правого брата
                while (lastRightBrother.RightBrother != null)
                    lastRightBrother = lastRightBrother.RightBrother;
                // сцепка деревьев

                lastRightBrother.RightBrother = shouldBecomeRightBrother;
                lastRightBrother = lastRightBrother.LeftChild;
                if (shouldBecomeRightBrother.LeftChild != null)
                {
                    shouldBecomeRightBrother = shouldBecomeRightBrother.LeftChild;
                }
                else
                {
                    while (shouldBecomeRightBrother.LeftChild == null)
                        shouldBecomeRightBrother = shouldBecomeRightBrother.RightBrother;
                    shouldBecomeRightBrother = shouldBecomeRightBrother.LeftChild;
                }
            } while (lastRightBrother != null);
            //return new BinomialTree<T>(first.root);
        }
    }
}
