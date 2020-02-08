using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace BinomialTreap.Common
{
    public static class SaverLoader
    {
        public static void SaveToFile(string fileName, object binomialHeap)
        {
            try
            {
                Stream stream = new FileStream(fileName, FileMode.OpenOrCreate);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, binomialHeap);
                stream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("GG");
                Console.ReadKey();
            }
        }

        public static object LoadFromFile(string fileName)
        {
            try
            {
                Stream stream = new FileStream(fileName, FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                object obj;
                obj = binaryFormatter.Deserialize(stream);
                stream.Close();
                return obj;
            }
            catch (Exception e)
            {
                Console.WriteLine("GG WP");
                Console.ReadKey();
            }
            return null;
        }
    }
}
