using System;

namespace RCWNeuralNetwork
{

    public class Matrix
    {
        public int Rows => rows;
        public int Columns => cols;

        private int rows;
        private int cols;
        private float[,] internalMatrix;
        private string name;
        private static Random rand = new Random();


        public Matrix(int gRows, int gCols, string name ="")
        {
            rows = gRows;
            cols = gCols;
            internalMatrix = new float[rows, cols];
            this.name = name;
            rand.Next();
        }

        public Matrix(int gRows, int gCols, float startVal, string name= "")
        {
            rows = gRows;
            cols = gCols;
            internalMatrix = new float[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] = startVal;
                }
            }
            this.name = name;
            rand.Next();
        }

        /// <summary>
        /// Puts in random values for each spot in the matrix.
        /// If no range is passed in then it defaults to using whole
        /// range of Random.Next()
        /// </summary>
        /// <param name="startRange">Start value of the rnadom range, inclusive. If passed by itself, acts as the end range of random range.</param>
        /// <param name="endRange">End value of the random range, exclusive.</param>
        public void RandomizeMatrix(int startRange = 0, int endRange = 0)
        {
            bool dontUseRange = startRange == 0 && endRange == 0;
            if(startRange > endRange)
            {
                int temp = startRange;
                startRange = endRange;
                endRange = temp;
            }
            rand.Next();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] = dontUseRange ? rand.Next() : rand.Next(startRange, endRange);
                }
            }
        }

        /// <summary>
        /// Randomize the values in the matrix, this time using Random.NextDouble(). Returns between zero
        /// and 1.  Use factor to increase this range, and shift to move the start and end value.
        /// </summary>
        /// <param name="factor">How much to scale up range of random numbers.</param>
        /// <param name="shift">How much to shift over set of random numbers, defaults to 0.</param>
        public void RandomizeMatrix(float factor, float shift = 0)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] = ((float)rand.NextDouble() * factor) - shift;
                }
            }
        }

        /// <summary>
        /// Set the named label of this matrix.
        /// </summary>
        /// <param name="name">What the new name will be.</param>
        public void SetName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Give back the name of this labeled matrix.
        /// By default the name is blank unless set.
        /// </summary>
        /// <returns>The string label that is set to this matrix.</returns>
        public string Name()
        {
            return this.name;
        }

        public float this[int key, int key2]
        {
            get
            {
                return internalMatrix[key,key2];
            }
            set
            {
                internalMatrix[key,key2] = value;
            }
        }

        /// <summary>
        /// Scale the matrix up for every element by multiplying by n.
        /// </summary>
        /// <param name="n">Float value n to multiply the elements by.</param>
        public void ScaleMatrix(float n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] *= n;
                }
            }
        }

        /// <summary>
        /// Scale the matrix up for every element by multiplying by n.
        /// </summary>
        /// <param name="n">Integer value n to multiply the elements by.</param>
        public void ScaleMatrix(int n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] *= n;
                }
            }
        }

        /// <summary>
        /// Scale the matrix up for every element by adding by n.
        /// </summary>
        /// <param name="n">Float value n to add to the elements. </param>
        public void AddConstant(float n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] += n;
                }
            }
        }

        /// <summary>
        /// Scale the matrix up for every element by adding by n.
        /// </summary>
        /// <param name="n">Integer value n to add to the elements. </param>
        public void AddConstant(int n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] += n;
                }
            }
        }

        /// <summary>
        /// Adds matricies element wise. Affects the one the method is called on.
        /// Only works if dimensions are the same.
        /// </summary>
        /// <param name="other">Other matrix being added to this matrix</param>
        /// <returns>True if successful, false if the dimensions weren't equal</returns>
        public bool AddTwoMatricies(Matrix other)
        {
            if (other.rows != this.rows || other.cols != this.cols)
            {
                //does nothing as they are not the same dimension
                return false;
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    this.internalMatrix[i, j] += other.internalMatrix[i, j];
                }
            }
            return true;
        }

        /// <summary>
        /// Subtract element wise two matricies and get back a new matrix.
        /// Matricies need to be same dimensions, otherwise returns null.
        /// </summary>
        /// <param name="first">Matrix to be subtracted from.</param>
        /// <param name="second">Matrix values that are doing the subtracting.</param>
        /// <returns>A new matrix where first - second happens.</returns>
        public static Matrix SubtractTwoMatricies(Matrix first, Matrix second)
        {
            if (first.rows != second.rows || first.cols != second.cols)
            {
                //does nothing as they are not the same dimension
                return null;
            }
            Matrix result = new Matrix(first.rows, first.cols, first.name + "-" + second.name);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {

                    result.internalMatrix[i, j] = first.internalMatrix[i, j] - second.internalMatrix[i,j];
                }
            }
            return result;
        }

        public void ElementWiseProduct(Matrix other)
        {
            if (this.rows != other.rows || this.cols != other.cols)
            {
                //does nothing as they are not the same dimension
                return;
            }
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {

                    this.internalMatrix[i, j] = this.internalMatrix[i, j] * other.internalMatrix[i, j];
                }
            }
        }

        public static Matrix ElementWiseProduct(Matrix first, Matrix second)
        {
            if (first.rows != second.rows || first.cols != second.cols)
            {
                //does nothing as they are not the same dimension
                return null;
            }
            Matrix result = new Matrix(first.rows, first.cols, first.name + "_times_" + second.name);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {

                    result.internalMatrix[i, j] = first.internalMatrix[i, j] * second.internalMatrix[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Perform matrix product on this matrix and one passed into the function.
        /// Returns a new matrix object that has the dimensions of this rows and
        /// the passed in columns. This columns and the other matrix rows must match
        /// each other.
        /// </summary>
        /// <param name="other">Matrix that is passed in to have applied to this current matrix.</param>
        /// <returns>New matrix that has the dot product applied to each of its vectors.</returns>
        public Matrix MatrixProduct(Matrix other)
        {
            if(this.cols != other.rows)
            {
                return null;
            }
            Matrix result = new Matrix(this.rows, other.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < other.rows; k++)
                    {
                        sum += internalMatrix[i, k] * other.internalMatrix[k, j];
                    }
                    result.internalMatrix[i, j] = sum;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix MatrixProduct(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.cols != matrix2.rows)
            {
                return null;
            }
            Matrix result = new Matrix(matrix1.rows, matrix2.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < matrix2.rows; k++)
                    {
                        sum += matrix1.internalMatrix[i, k] * matrix2.internalMatrix[k, j];
                    }
                    result.internalMatrix[i, j] = sum;
                }
            }
            return result;
        }

        /// <summary>
        /// Rotate matrix so that rows are now columns.
        /// </summary>
        public void TransposeMatrix()
        {
            Matrix result = new Matrix(this.cols, this.rows, this.name);
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    result.internalMatrix[j, i] = this.internalMatrix[i,j];
                }
            }
            this.rows = result.rows;
            this.cols = result.cols;
            this.internalMatrix = result.internalMatrix;
            this.name = result.name;
        }

        /// <summary>
        /// Rotate matrix so that rows are now columns.
        /// </summary>
        /// <returns>New matrix that has rows which are equal to original matrix columns and the same for the new columns.</returns>
        public static Matrix TransposeMatrix(Matrix given)
        {
            Matrix result = new Matrix(given.cols, given.rows, given.name);
            for (int i = 0; i < given.rows; i++)
            {
                for (int j = 0; j < given.cols; j++)
                {
                    result.internalMatrix[j, i] = given.internalMatrix[i, j];
                }
            }
            return result;
        }

        public delegate float MatrixFunc2(float val, int i, int j);


        /// <summary>
        /// Takes in a function delegate of the signature 'float FuncName(float, int, int)' 
        /// and applies the function to each value in the matrix. This is an in place matrix
        /// operation.
        /// </summary>
        /// <param name="func">Delegate function that is applied to each element in the matrix. Return type float, arg1=float, arg2=int, arg3=int</param>
        public void Apply(ActivationFunction func)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.internalMatrix[i, j] = func(this.internalMatrix[i, j]);
                }
            }
        }

        /// <summary>
        /// Takes in a function delegate of the signature 'float FuncName(float, int, int)' 
        /// and applies the function to each value in the matrix. This is an in place matrix
        /// operation.
        /// </summary>
        /// <param name="func">Delegate function that is applied to each element in the matrix. Return type float, arg1=float, arg2=int, arg3=int</param>
        public void Apply(MatrixFunc2 func)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.internalMatrix[i, j] = func(this.internalMatrix[i, j], i, j);
                }
            }
        }

        /// <summary>
        /// Takes in a function delegate of the signature 'float FuncName(float, int, int)' 
        /// and applies the function to each value in the matrix. This returns a new matrix.
        /// </summary>
        /// <param name="func">Delegate function that is applied to each element in the matrix. Return type float, arg1=float, arg2=int, arg3=int</param>
        /// <returns>New matrix returned that was passed in matrix with func applied to all values.</returns>
        public static Matrix Apply(Matrix mat, ActivationFunction func)
        {
            Matrix result = new Matrix(mat.rows, mat.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.internalMatrix[i, j] = func(mat.internalMatrix[i, j]);
                }
            }
            return result;
        }

        /// <summary>
        /// Takes in a function delegate of the signature 'float FuncName(float, int, int)' 
        /// and applies the function to each value in the matrix. This returns a new matrix.
        /// </summary>
        /// <param name="func">Delegate function that is applied to each element in the matrix. Return type float, arg1=float, arg2=int, arg3=int</param>
        /// <returns>New matrix returned that was passed in matrix with func applied to all values.</returns>
        public static Matrix Apply(Matrix mat, MatrixFunc2 func)
        {
            Matrix result = new Matrix(mat.rows, mat.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    result.internalMatrix[i, j] = func(mat.internalMatrix[i, j], i, j);
                }
            }
            return result;
        }

        /// <summary>
        /// Creates an <see cref="Matrix"/> object from the float array as a single column matrix.
        /// </summary>
        /// <param name="inputs">Input values of matrix.</param>
        /// <param name="label">Name of the matrix (optional).</param>
        /// <returns><see cref="Matrix"/> object as a single column of given float array.</returns>
        public static Matrix FromArray(float[] inputs, string label = "")
        {
            Matrix output = new Matrix(inputs.Length, 1, label);
            for (int i = 0; i < inputs.Length; i++)
            {
                output.internalMatrix[i, 0] = inputs[i];
            }
            return output;
        }

        public float[] ToArray()
        {
            float[] res = new float[this.cols * this.rows];
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    res[this.cols * i + j] = this.internalMatrix[i, j];
                }
            }
            return res;
        }


        public override string ToString()
        {
            string answer = "Matrix " + name + ": \n";
            for (int i = 0; i < rows; i++)
            {
                answer += i + ": ";
                for (int j = 0; j < cols; j++)
                {
                    answer += internalMatrix[i, j] + " ";
                }
                answer += "\n";
            }
            return answer;
        }

        public static Matrix operator+(Matrix left, Matrix right)
        {
            Matrix output = new Matrix(left.rows, left.cols);
            for (int i = 0; i < output.rows; i++)
            {
                for (int j = 0; j < output.cols; j++)
                {
                    output.internalMatrix[i, j] = left.internalMatrix[i, j] + right.internalMatrix[i, j];
                }
            }
            return output;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            if (left.rows != right.rows || left.cols != right.cols)
            {
                //does nothing as they are not the same dimension
                return null;
            }
            Matrix output = new Matrix(left.rows, left.cols, left.name + "-" + right.name);
            for (int i = 0; i < output.rows; i++)
            {
                for (int j = 0; j < output.cols; j++)
                {
                    output.internalMatrix[i, j] = left.internalMatrix[i, j] - right.internalMatrix[i, j];
                }
            }
            return output;
        }
    }
}
