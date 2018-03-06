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
            matrix2.RandomizeMatrix(0, 20);
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

            NeuralNetwork nn = new NeuralNetwork(2, 2, 1);
            float[][] inputs = new float[4][];
            float[][] targets = new float[4][];
            float num1 = 0;
            float num2 = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = new float[2];
                targets[i] = new float[1];
                targets[i][0] = (num1 == 1 || num2 == 1) && i < inputs.Length - 1 ? 1 : 0;

                for (int j = 0; j < inputs[i].Length; j++)
                {
                    inputs[i][j] = j == 0 ? num1 : num2;
                }
                num2++;
                if (num2 > 1)
                {
                    num2 = 0;
                    num1++;
                }
            }

            Random rand = new Random();
            rand.Next();
            for (int count = 0; count < 500000; count++)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    int idx = rand.Next(0, 4);
                    nn.TrainNetwork(inputs[idx], targets[idx]);
                }

                if(count % 10000 == 0)
                {
                    Console.WriteLine();
                    float[] guessI = nn.FeedForward(new float[] { 0, 0 });
                    Console.WriteLine(guessI[0]);
                    guessI = nn.FeedForward(new float[] { 1, 0 });
                    Console.WriteLine(guessI[0]);
                    guessI = nn.FeedForward(new float[] { 0, 1 });
                    Console.WriteLine(guessI[0]);
                    guessI = nn.FeedForward(new float[] { 1, 1 });
                    Console.WriteLine(guessI[0]);
                }                
            }
            Console.WriteLine();
            float[] guess = nn.FeedForward(new float[] { 0, 0 });
            Console.WriteLine(guess[0]);
            guess = nn.FeedForward(new float[] { 1, 0 });
            Console.WriteLine(guess[0]);
            guess = nn.FeedForward(new float[] { 0, 1 });
            Console.WriteLine(guess[0]);
            guess = nn.FeedForward(new float[] { 1, 1 });
            Console.WriteLine(guess[0]);

            /*float[] output = nn.FeedForward(input);
            foreach(float ans in output)
            {
                Console.WriteLine(ans);
            }*/
        }
    }
}
