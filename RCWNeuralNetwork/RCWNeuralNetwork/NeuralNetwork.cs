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

        private float learningRate;

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
            learningRate = 0.1f; 
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

        public NeuralNetwork(int numI, int numH, int numO, float rate = 0.1f, int layers = 1)
        {
            numInputNodes = numI;
            numHiddenNodes = numH;
            numOutputNodes = numO;
            numHiddenLayers = layers;
            learningRate = rate;
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
        /// <param name="rate"></param>
        public void SetLearningRate(float rate)
        {
            learningRate = rate;
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

        
        public void TrainNetwork(float[] inputsArr, float[] targetsArr)
        {
            MyMatrix inputMat = MyMatrix.FromArray(inputsArr);
            MyMatrix inputHidden = MyMatrix.MatrixProduct(this.weightsInputHidden, inputMat);
            if (!inputHidden.AddTwoMatricies(this.biasHidden))
            {
                Console.WriteLine("matricies didn't add 1");
            }
            inputHidden.ApplyFuncToMatrix(SigmoidActivation);

            MyMatrix outputMat = MyMatrix.MatrixProduct(this.weightsHiddenOutput, inputHidden);
            if (!outputMat.AddTwoMatricies(this.biasOutput))
            {
                Console.WriteLine("matricies didn't add 2");
            }
            outputMat.ApplyFuncToMatrix(SigmoidActivation);
            outputMat.SetName("outputs");
            //change to matricies
            MyMatrix targetMat = MyMatrix.FromArray(targetsArr, "targets");

            //calculate error: Error = Targets - Outputs
            MyMatrix outputErrors = MyMatrix.SubtractTwoMatricies(targetMat, outputMat);
            //calculate gradient
            MyMatrix outputGradientMat = MyMatrix.ApplyFuncToMatrix(outputMat, FakeDerivativeOfSigmoid);
            outputGradientMat.ElementWiseProduct(outputErrors);
            outputGradientMat.ScaleMatrix(this.learningRate);

            //calculate deltas
            MyMatrix hiddenTranspose = MyMatrix.TransposeMatrix(inputHidden);
            MyMatrix weightHiddenOutputDeltas = MyMatrix.MatrixProduct(outputGradientMat, hiddenTranspose);

            //adjust the weights by deltas
            this.weightsHiddenOutput.AddTwoMatricies(weightHiddenOutputDeltas);
            //adjust bias by gradient
            this.biasOutput.AddTwoMatricies(outputGradientMat);

            MyMatrix weightsHiddenOutputTranspose = MyMatrix.TransposeMatrix(this.weightsHiddenOutput);
            MyMatrix hiddenErrors = MyMatrix.MatrixProduct(weightsHiddenOutputTranspose, outputErrors);

            //calculate hidden gradient
            MyMatrix hiddenGradientMat = MyMatrix.ApplyFuncToMatrix(inputHidden, FakeDerivativeOfSigmoid);
            hiddenGradientMat.ElementWiseProduct(hiddenErrors);
            hiddenGradientMat.ScaleMatrix(this.learningRate);

            //calculate input to hidden deltas
            MyMatrix inputsTransposed = MyMatrix.TransposeMatrix(inputMat);
            MyMatrix weightsInputHiddenDeltas = MyMatrix.MatrixProduct(hiddenGradientMat, inputsTransposed);

            //adjust the weights by deltas
            this.weightsInputHidden.AddTwoMatricies(weightsInputHiddenDeltas);
            //adjust bias by gradient
            this.biasHidden.AddTwoMatricies(hiddenGradientMat);
        }


        #region Activation Functions
        /*ACTIVIATION FUNCTIONS*/
        public static float SigmoidActivation(float x)
        {
            return 1 / (1 + (float)Math.Exp(-x));
        }

        public static float FakeDerivativeOfSigmoid(float x)
        {
            return x * (1 - x);
        }
        #endregion
    }
}
