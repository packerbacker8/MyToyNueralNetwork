using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Perceptron
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Perceptron percept = new Perceptron(3);
            Point[] points = new Point[100];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point(); 
            }

            int count = 0;
            int correct = 0;

            for (int i = 0; i < points.Length; i++)
            {
                if(points[i].label > 0)
                {
                    Console.Write(points[i].ShowPoint() + " ");
                    count++;
                }
                if(count == 5)
                {
                    Console.WriteLine();
                    count = 0;
                }
                //check without training
                float[] tPoints = new float[] { points[i].x, points[i].y , points[i].bias};
                if (percept.TakeAGuess(tPoints) == points[i].label)
                {
                    correct++;
                }
            }
            Console.WriteLine();
            float percent = (float)correct / points.Length;
            Console.WriteLine("Percent correct without training out of " + points.Length + " attempts: " + percent);

            Console.WriteLine();
            Console.WriteLine();
            count = 0;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].label < 0)
                {
                    Console.Write(points[i].ShowPoint() + " ");
                    count++;
                }
                if (count == 5)
                {
                    Console.WriteLine();
                    count = 0;
                }
            }
            Console.WriteLine();
            percent = 0;
            while (percent < 1)
            {
                correct = 0;
                for (int i = 0; i < points.Length; i++)
                {
                    float[] tPoints = new float[] { points[i].x, points[i].y, points[i].bias };
                    percept.TrainPerceptron(tPoints, points[i].label);

                    if (percept.TakeAGuess(tPoints) == points[i].label)
                    {
                        //Console.WriteLine("Correct");
                        correct++;
                    }
                }
                percent = (float)correct / points.Length;
                Console.WriteLine("Percent correct with training out of " + points.Length + " attempts: " + percent);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PerceptronForm myForm = new PerceptronForm();
            
            Application.Run(myForm);
        }
    }
}
