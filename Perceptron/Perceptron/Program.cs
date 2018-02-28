using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            Perceptron p = new Perceptron();
            float[] inputs = { -1, 0.5f };
            int guess = p.TakeAGuess(inputs);
            Console.WriteLine(guess);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 myForm = new Form1();
            Application.Run(myForm);
        }
    }
}
