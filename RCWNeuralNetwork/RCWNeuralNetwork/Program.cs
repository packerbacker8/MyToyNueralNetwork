using System;
using System.Collections.Generic;

namespace RCWNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(2, 2, 1);
            List<TrainingData> trainingData = new List<TrainingData>()
            {
                new TrainingData(new float[] {0,1}, new float[] {1}),
                new TrainingData(new float[] {1,0}, new float[] {1}),
                new TrainingData(new float[] {0,0}, new float[] {0}),
                new TrainingData(new float[] {1,1}, new float[] {0})
            };
            PrintArray(nn.FeedForward(new float[] { 1, 0 }));
            PrintArray(nn.FeedForward(new float[] { 0, 0 }));
            PrintArray(nn.FeedForward(new float[] { 1, 1 }));
            PrintArray(nn.FeedForward(new float[] { 0, 1 }));
            for (int i = 0; i < 100000; i++)
            {
                trainingData.Shuffle();
                for (int j = 0; j < trainingData.Count; j++)
                {
                    TrainingData data = trainingData[j];
                    nn.TrainNetwork(data.inputs, data.targets);
                }
            }
            PrintArray(nn.FeedForward(new float[] { 1, 0 }));
            PrintArray(nn.FeedForward(new float[] { 0, 0 }));
            PrintArray(nn.FeedForward(new float[] { 1, 1 }));
            PrintArray(nn.FeedForward(new float[] { 0, 1 }));
        }

        private static void PrintArray(float[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i}: {arr[i]}");
            }
        }

        private static float TanHActivation(float x)
        {
            return (float)Math.Tanh(x);
        }

        private static float TanHDerivative(float x)
        {
            float tanh = TanHActivation(x);
            return 1 - (tanh * tanh);
        }
    }

    struct TrainingData
    {
        public TrainingData(float[] inputs, float[] targets)
        {
            this.inputs = inputs;
            this.targets = targets;
        }
        public float[] inputs;
        public float[] targets;
    }
}
