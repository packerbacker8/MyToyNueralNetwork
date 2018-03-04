using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCWNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMatrix matrix = new MyMatrix(4, 3, 1);
            matrix.ScaleMatrix(4);
            MyMatrix matrix2 = new MyMatrix(4, 3, 2);
            matrix2.RandomizeMatrix(0,20);
            matrix += matrix2;
            Console.WriteLine(matrix.ToString());

            MyMatrix a = new MyMatrix(2, 2, "A");
            a.RandomizeMatrix(16);
            MyMatrix b = new MyMatrix(2, 2, "B");
            b.RandomizeMatrix(0, 16);
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            MyMatrix c = MyMatrix.MatrixProduct(a, b);
            MyMatrix c2 = a.MatrixProduct(b);
            c.SetName("C");
            c2.SetName("C2");
            Console.WriteLine(c.ToString());
            Console.WriteLine(c2.ToString());
            MyMatrix d = new MyMatrix(4, 3, "D");
            d.RandomizeMatrix(-5, 29);
            Console.WriteLine(d.ToString());
            d.TransposeMatrix();
            Console.WriteLine(d.ToString());
            d.ApplyFuncToMatrix((x, i, j) => i > j ? x * 2.2f : x + 3.1f);
            Console.WriteLine(d.ToString());
        }
    }
}
