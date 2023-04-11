using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class Training
    {
    }

    class Point
    {
        public static float lineM = 0.78f;
        public static float lineB = -0.1f;
        public float x;
        public float y;
        public float bias = 1f;
        public int label;
        private static Random rand = new Random();
        public Point()
        {
            rand.Next();
            x = (float)(rand.NextDouble() * 2) - 1.0f;
            y = (float)(rand.NextDouble() * 2) - 1.0f;
            float lineY = LineFunc(lineM, x, lineB);
            label = y >= lineY ? 1 : -1;
        }

        public Point(float givenX, float givenY)
        {
            x = givenX;
            y = givenY;
            float lineY = LineFunc(lineM, x, lineB);
            label = y >= lineY ? 1 : -1;
        }

        public String ShowPoint()
        {
            return ((label * x) + "," + (label * y));
        }

        public float LineFunc(float m, float x, float b)
        {
            return m * x + b;
        }
    }
}
