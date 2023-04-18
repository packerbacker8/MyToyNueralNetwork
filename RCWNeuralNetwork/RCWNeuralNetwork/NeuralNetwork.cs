using System;

namespace RCWNeuralNetwork
{
    public delegate float ActivationFunction(float x);

    public class NeuralNetwork
    {
        private int numInputNodes;
        private int numHiddenNodes;
        private int numHiddenLayers;
        private int numOutputNodes;

        private float learningRate;

        private Matrix weightsInputHidden;
        private Matrix[] weightsInputHiddenArr;
        private Matrix weightsHiddenOutput;
        private Matrix biasHidden;
        private Matrix biasOutput;

        /// <summary>
        /// Default constructor. Gives 2 input nodes, 2 hidden, and 1 output.
        /// </summary>
        public NeuralNetwork()
        {
            Setup(2, 2, 1);
        }

        public NeuralNetwork(int numI, int numH, int numO, float rate = 0.1f, int layers = 1)
        {
            Setup(numI, numH, numO, rate, layers);
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
        /// <param name="inputArray"></param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputArray)
        {
            Matrix input = Matrix.FromArray(inputArray);
            return FeedForward(input, SigmoidActivation);
        }

        public float[] FeedForward(float[] inputArray, ActivationFunction func)
        {
            Matrix input = Matrix.FromArray(inputArray);
            return FeedForward(input, func);
        }

        public float[] FeedForward(Matrix input)
        {
            return FeedForward(input, SigmoidActivation);
        }

        public float[] FeedForward(Matrix input, ActivationFunction func)
        {
            return FeedForwardMatrix(input, func).ToArray();
        }

        private Matrix FeedForwardMatrix(Matrix input, ActivationFunction func)
        {
            Matrix hidden = Matrix.MatrixProduct(this.weightsInputHidden, input);
            if (!hidden.AddTwoMatricies(this.biasHidden))
            {
                Console.WriteLine("bias was not added successfully to hidden");
            }
            hidden.Apply(func);

            Matrix result = Matrix.MatrixProduct(this.weightsHiddenOutput, hidden);
            if (!result.AddTwoMatricies(this.biasOutput))
            {
                Console.WriteLine("bias not added successfully to output");
            }
            result.Apply(func);
            return result;
        }

        public void TrainNetwork(float[] inputsArr, float[] targetsArr)
        {
            TrainNetwork(inputsArr, targetsArr, SigmoidActivation, FakeDerivativeOfSigmoid);
        }

        public void TrainNetwork(float[] inputsArr, float[] targetsArr, ActivationFunction activator, ActivationFunction derivative)
        {
            Matrix inputMat = Matrix.FromArray(inputsArr, "inputs");
            Matrix targetMat = Matrix.FromArray(targetsArr, "targets");
            TrainNetwork(inputMat, targetMat, activator, derivative);
        }

        public void TrainNetwork(Matrix inputMat, Matrix targetMat)
        {
            TrainNetwork(inputMat, targetMat, SigmoidActivation, FakeDerivativeOfSigmoid);
        }

        public void TrainNetwork(Matrix inputMat, Matrix targetMat, ActivationFunction activator, ActivationFunction derivative)
        {
            Matrix inputHidden = Matrix.MatrixProduct(this.weightsInputHidden, inputMat);
            if (!inputHidden.AddTwoMatricies(this.biasHidden))
            {
                Console.WriteLine("matricies didn't add 1");
            }
            inputHidden.Apply(activator);

            Matrix outputMat = Matrix.MatrixProduct(this.weightsHiddenOutput, inputHidden);
            if (!outputMat.AddTwoMatricies(this.biasOutput))
            {
                Console.WriteLine("matricies didn't add 2");
            }
            outputMat.Apply(activator);
            outputMat.SetName("outputs");

            //calculate error: Error = Targets - Outputs
            Matrix outputErrors = targetMat - outputMat;
            outputErrors.SetName("outputErrors");

            //calculate gradient
            Matrix outputGradientMat = Matrix.Apply(outputMat, derivative);
            outputGradientMat.ElementWiseProduct(outputErrors);
            outputGradientMat.ScaleMatrix(this.learningRate);

            //calculate deltas
            Matrix hiddenTranspose = Matrix.TransposeMatrix(inputHidden);
            Matrix weightHiddenOutputDeltas = Matrix.MatrixProduct(outputGradientMat, hiddenTranspose);

            //adjust the weights by deltas
            this.weightsHiddenOutput.AddTwoMatricies(weightHiddenOutputDeltas);
            //adjust bias by gradient
            this.biasOutput.AddTwoMatricies(outputGradientMat);

            Matrix weightsHiddenOutputTranspose = Matrix.TransposeMatrix(this.weightsHiddenOutput);
            Matrix hiddenErrors = Matrix.MatrixProduct(weightsHiddenOutputTranspose, outputErrors);

            //calculate hidden gradient
            Matrix hiddenGradientMat = Matrix.Apply(inputHidden, derivative);
            hiddenGradientMat.ElementWiseProduct(hiddenErrors);
            hiddenGradientMat.ScaleMatrix(this.learningRate);

            //calculate input to hidden deltas
            Matrix inputsTransposed = Matrix.TransposeMatrix(inputMat);
            Matrix weightsInputHiddenDeltas = Matrix.MatrixProduct(hiddenGradientMat, inputsTransposed);

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

        private void Setup(int numI, int numH, int numO, float rate = 0.1f, int layers = 1)
        {
            numInputNodes = numI;
            numHiddenNodes = numH;
            numOutputNodes = numO;
            numHiddenLayers = layers;
            learningRate = rate;
            weightsInputHidden = new Matrix(numHiddenNodes, numInputNodes, "IH");
            weightsInputHiddenArr = new Matrix[numHiddenLayers];
            for(int i = 0; i < numHiddenLayers; i++)
            {
                weightsInputHiddenArr[i] = new Matrix(numHiddenNodes, numInputNodes, "IH" + i);
            }
            weightsHiddenOutput = new Matrix(numOutputNodes, numHiddenNodes, "HO");
            biasHidden = new Matrix(numHiddenNodes, 1, "BH");
            biasOutput = new Matrix(numOutputNodes, 1, "BO");
            weightsInputHidden.RandomizeMatrix(2f, 1f);
            weightsHiddenOutput.RandomizeMatrix(2f, 1f);
            biasHidden.RandomizeMatrix(2f, 1f);
            biasOutput.RandomizeMatrix(2f, 1f);
        }
    }
}
