using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinomialTreap
{
    class Program
    {
        static void Main(string[] args)
        {
            BinomialHeap<int> binomialHeap = new BinomialHeap<int>();
            binomialHeap.Add(15);
            binomialHeap.Add(10);
            binomialHeap.Add(8);
            binomialHeap.Add(7);
            binomialHeap.Add(12);
            binomialHeap.Add(13);
            binomialHeap.Add(40);
            binomialHeap.Add(25);
            binomialHeap.Add(3);
            binomialHeap.Print();

            binomialHeap.PrintFirstNaturalElements(4);
        }
    }
}
