using System;
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

        private MyMatrix weightsInputHidden;
        //private MyMatrix[] weightsInputHidden;  //will need to set this up when doing many layers
        private MyMatrix weightsHiddenOutput;
        private MyMatrix biasHidden;
        private MyMatrix biasOutput;

        /// <summary>
        /// Default constructor. Gives 2 input nodes, 2 hidden, and 1 output.
        /// </summary>
        public NeuralNetwork()
        {
            numInputNodes = 2;
            numHiddenNodes = 2;
            numHiddenLayers = 1;
            numOutputNodes = 1;
            weightsInputHidden = new MyMatrix(numHiddenNodes, numInputNodes, "IH");
            /*weightsInputHidden = new MyMatrix[numHiddenLayers];
            for(int i = 0; i < numHiddenLayers; i++)
            {
                weightsInputHidden[i] = new MyMatrix(numHiddenNodes, numInputNodes, "IH" + i);
            }*/
            weightsHiddenOutput = new MyMatrix(numOutputNodes, numHiddenNodes, "HO");
            biasHidden = new MyMatrix(numHiddenNodes, 1, "BH");
            biasOutput = new MyMatrix(numOutputNodes, 1, "BO");
            weightsInputHidden.RandomizeMatrix(2f, 1f);
            weightsHiddenOutput.RandomizeMatrix(2f, 1f);
        }

        public NeuralNetwork(int numI, int numH, int numO, int layers = 1)
        {
            numInputNodes = numI;
            numHiddenNodes = numH;
            numOutputNodes = numO;
            numHiddenLayers = layers;
            weightsInputHidden = new MyMatrix(numHiddenNodes, numInputNodes, "IH");
            /*weightsInputHidden = new MyMatrix[numHiddenLayers];
            for(int i = 0; i < numHiddenLayers; i++)
            {
                weightsInputHidden[i] = new MyMatrix(numHiddenNodes, numInputNodes, "IH" + i);
            }*/
            weightsHiddenOutput = new MyMatrix(numOutputNodes, numHiddenNodes, "HO");
            biasHidden = new MyMatrix(numHiddenNodes, 1, "BH");
            biasOutput = new MyMatrix(numOutputNodes, 1, "BO");
            weightsInputHidden.RandomizeMatrix(2f, 1f);
            weightsHiddenOutput.RandomizeMatrix(2f, 1f);
            biasHidden.RandomizeMatrix(2f, 1f);
            biasOutput.RandomizeMatrix(2f, 1f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input_array"></param>
        /// <returns></returns>
        public float[] FeedForward(float[] input_array)
        {
            MyMatrix input = MyMatrix.FromArray(input_array);
            MyMatrix hidden = MyMatrix.MatrixProduct(this.weightsInputHidden, input);
            if(!hidden.AddTwoMatricies(this.biasHidden))
            {
                Console.WriteLine("matricies didn't add 1");
            }
            hidden.ApplyFuncToMatrix(SigmoidActivation);

            MyMatrix result = MyMatrix.MatrixProduct(this.weightsHiddenOutput, hidden);
            if(!result.AddTwoMatricies(this.biasOutput))
            {
                Console.WriteLine("matricies didn't add 2");
            }
            result.ApplyFuncToMatrix(SigmoidActivation);
            return result.ToArray();
        }

        public void TrainNetwork(float[] inputs, float[] targets)
        {
            float[] output = this.FeedForward(inputs);

            //change to matricies
            MyMatrix outputMat = MyMatrix.FromArray(output, "outputs");
            MyMatrix targetMat = MyMatrix.FromArray(targets, "targets");

            //calculate error: Error = Targets - Outputs
            MyMatrix errors = MyMatrix.SubtractTwoMatricies(targetMat, outputMat);

            //transpose hidden output weights before getting hidden errors
            MyMatrix weightsHiddenOutputTranspose = MyMatrix.TransposeMatrix(this.weightsHiddenOutput);
            MyMatrix hiddenErrors = MyMatrix.MatrixProduct(weightsHiddenOutputTranspose, errors);
        }


        /*ACTIVIATION FUNCTIONS*/
        public static float SigmoidActivation(float x)
        {
            return 1 / (1 + (float)Math.Exp(-x));
        }
    }
}
