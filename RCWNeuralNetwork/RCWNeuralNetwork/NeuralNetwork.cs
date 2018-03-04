﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCWNeuralNetwork
{
    class NeuralNetwork
    {
        private int numInputNodes;
        private int numHiddenNodes;
        private int numHiddenLayers;
        private int numOutputNodes;

        /// <summary>
        /// Default constructor. Gives 2 input nodes, 2 hidden, and 1 output.
        /// </summary>
        public NeuralNetwork()
        {
            numInputNodes = 2;
            numHiddenNodes = 2;
            numHiddenLayers = 1;
            numOutputNodes = 1;
        }

        public NeuralNetwork(int numI, int numH, int numO, int layers = 1)
        {
            numInputNodes = numI;
            numHiddenNodes = numH;
            numOutputNodes = numO;
            numHiddenLayers = layers;
        }
    }
}
