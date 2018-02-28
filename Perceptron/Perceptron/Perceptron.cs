﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class Perceptron
    {
        float[] weights;

        /// <summary>
        /// Perceptron is a basic single input classification class. Takes number of weights and returns output
        /// of 1 or -1 based on the classification it comes up with.
        /// </summary>
        /// <param name="numWeights">How many weights should there be? Defaults to 2 if none passed.</param>
        public Perceptron(int numWeights = 2)
        {
            weights = new float[numWeights];
            Random rand = new Random();
            rand.Next();
            for(int i = 0; i < weights.Length; i++)
            {
                weights[i] = (float)((rand.NextDouble() * 2) - 1);
            }
        }

        /// <summary>
        /// Perceptron attempts to classify the input array.
        /// </summary>
        /// <param name="inputs">Array input of values.</param>
        /// <returns>Returns either 1 or -1 depending on classification calculated.</returns>
        public int TakeAGuess(float[] inputs)
        {
            float sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }

            int output = GetSign(sum);

            return output;
        }

        /// <summary>
        /// Checks if the value is greater than or equal to zero and returns either 1 or -1.
        /// </summary>
        /// <param name="value">The value to be checked.</param>
        /// <returns>If value is less than 0, returns -1, otherwise 1.</returns>
        public int GetSign(float value)
        {
            return value >= 0 ? 1 : -1;
        }
    }

}