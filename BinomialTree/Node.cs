using System;

namespace BinomialTreap.BinomialTree
{
    /// <summary>
    /// Узел биномиального дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> where T : IComparable<T>
    {
        

        /// <summary>
        /// Ключ
        /// </summary>
        public int Key { get; set; }
        /// <summary>
        /// Данные
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Левый потомок
        /// </summary>
        public Node<T> LeftChild { get; set; }
        /// <summary>
        /// Правый брат
        /// </summary>
        public Node<T> RightBrother { get; set; }
        /// <summary>
        /// Родительский узел
        /// </summary>
        public Node<T> Parent { get; set; }

        /// <summary>
        /// Степень узла
        /// </summary>
        public int Degree { get; set; }

        public Node(int key, T data = default(T))
        {
            Degree = 0;
            Key = key;
            Data = data;
            Parent = LeftChild = RightBrother = null;
        }

        public Node(int key, T data = default(T), Node<T> parent = null, Node<T> leftChild = null, Node<T> rightChild = null) : this(key, data)
        {
            Parent = parent;
            LeftChild = leftChild;
            RightBrother = rightChild;
        }

        public void PrintNode()
        {
            if (RightBrother != null)
                Console.Write(Data + "-->");
            else
            {
                Console.Write(Data);               
                Console.WriteLine("\n");               
            }
        }

        /// <summary>
        /// Показывает, является ли Data натуральным числом
        /// </summary>
        /// <returns></returns>
        public bool IsNatural()
        {
            int data = (int)(object)Data;
            try
            {
                string dataString = Convert.ToString(data);
                for (int i = 0; i < dataString.Length; i++)
                {
                    if (dataString[i] < '0' || dataString[i] > '9')
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Невозможно преобразовать data в string");
            }
            return true;
        }
    }
}
