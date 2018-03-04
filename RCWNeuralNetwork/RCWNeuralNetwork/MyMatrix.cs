using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCWNeuralNetwork
{

    class MyMatrix
    {
        private int rows;
        private int cols;
        private float[,] internalMatrix;
        private string name;
        private static Random rand = new Random();


        public MyMatrix(int gRows, int gCols, string name ="")
        {
            rows = gRows;
            cols = gCols;
            internalMatrix = new float[rows, cols];
            this.name = name;
        }

        public MyMatrix(int gRows, int gCols, float startVal, string name= "")
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
        public bool AddTwoMatricies(MyMatrix other)
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
        /// Perform matrix product on this matrix and one passed into the function.
        /// Returns a new matrix object that has the dimensions of this rows and
        /// the passed in columns. This columns and the other matrix rows must match
        /// each other.
        /// </summary>
        /// <param name="other">Matrix that is passed in to have applied to this current matrix.</param>
        /// <returns>New matrix that has the dot product applied to each of its vectors.</returns>
        public MyMatrix MatrixProduct(MyMatrix other)
        {
            if(this.cols != other.rows)
            {
                return null;
            }
            MyMatrix result = new MyMatrix(this.rows, other.cols);
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
        public static MyMatrix MatrixProduct(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.cols != matrix2.rows)
            {
                return null;
            }
            MyMatrix result = new MyMatrix(matrix1.rows, matrix2.cols);
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
            MyMatrix result = new MyMatrix(this.cols, this.rows, this.name);
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
        public static MyMatrix TransposeMatrix(MyMatrix given)
        {
            MyMatrix result = new MyMatrix(given.cols, given.rows, given.name);
            for (int i = 0; i < given.rows; i++)
            {
                for (int j = 0; j < given.cols; j++)
                {
                    result.internalMatrix[j, i] = given.internalMatrix[i, j];
                }
            }
            return result;
        }

        public delegate float MatrixFunc(float val, int i, int j);

        /// <summary>
        /// Takes in a function delegate of the signature 'float FuncName(float, int, int)' 
        /// and applies the function to each value in the matrix. This is an in place matrix
        /// operation.
        /// </summary>
        /// <param name="func">Dlegate function that is applied to each element in the matrix. Return type float, arg1=float, arg2=int, arg3=int</param>
        public void ApplyFuncToMatrix(MatrixFunc func)
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
        /// Number of rows in the matrix.
        /// </summary>
        /// <returns>Number of rows in the matrix.</returns>
        public int Rows()
        {
            return rows;
        }

        /// <summary>
        /// Number of columns in the matrix.
        /// </summary>
        /// <returns>Number of columns in the matrix.</returns>
        public int Columns()
        {
            return cols;
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

        public static MyMatrix operator+(MyMatrix left, MyMatrix right)
        {
            MyMatrix output = new MyMatrix(left.rows, left.cols);
            for (int i = 0; i < output.rows; i++)
            {
                for (int j = 0; j < output.cols; j++)
                {
                    output.internalMatrix[i, j] = left.internalMatrix[i, j] + right.internalMatrix[i, j];
                }
            }
            return output;
        }

        public static MyMatrix operator -(MyMatrix left, MyMatrix right)
        {
            MyMatrix output = new MyMatrix(left.rows, left.cols);
            for (int i = 0; i < output.rows; i++)
            {
                for (int j = 0; j < output.cols; j++)
                {
                    output.internalMatrix[i, j] = left.internalMatrix[i, j] + right.internalMatrix[i, j];
                }
            }
            return output;
        }
    }

    /*
    class MyMatrix<T>
    {
        private int rows;
        private int cols;
        private T[,] internalMatrix;

        public MyMatrix(int gRows, int gCols)
        {
            rows = gRows;
            cols = gCols;
            internalMatrix = new T[rows, cols];
        }

        public MyMatrix(int gRows, int gCols, T startVal)
        {
            rows = gRows;
            cols = gCols;
            internalMatrix = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i,j] = startVal;
                }
            }
        }

        public void RandomizeMatrix()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    internalMatrix[i, j] = startVal;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="iIdx"></param>
        /// <param name="jIdx"></param>
        public void SetValue(T val, int iIdx, int jIdx)
        {
            internalMatrix[iIdx, jIdx] = val;
        }

        public T GetValue(int iIdx, int jIdx)
        {
            return internalMatrix[iIdx, jIdx];
        }

        public void ScaleMatrix(T n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    dynamic curVal = internalMatrix[i, j];
                    dynamic nVal = n;
                    internalMatrix[i, j] = curVal * nVal;
                }
            }
        }

        public void AddConstant(T n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    dynamic curVal = internalMatrix[i, j];
                    dynamic nVal = n;
                    internalMatrix[i, j] = curVal + nVal;
                }
            }
        }

        /// <summary>
        /// Adds matricies element wise. Affects the one the method is called on.
        /// Only works if dimensions are the same.
        /// </summary>
        /// <param name="other">Other matrix being added to this matrix</param>
        /// <returns>True if successful, false if the dimensions weren't equal</returns>
        public bool AddTwoMatricies(MyMatrix<T> other)
        {
            if(other.rows != this.rows || other.cols != this.cols)
            {
                //does nothing as they are not the same dimension
                return false;
            }
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    dynamic firstVal = this.internalMatrix[i, j];
                    dynamic secodVal = other.internalMatrix[i,j];
                    this.internalMatrix[i, j] = firstVal + secodVal;
                }
            }
            return true;
        }

        public int Rows()
        {
            return rows;
        }

        public int Columns()
        {
            return cols;
        }

        public override string ToString()
        {
            string answer = "Matrix: \n";
            for (int i = 0; i < rows; i++)
            {
                answer += i + ": ";
                for (int j = 0; j < cols; j++)
                {
                    dynamic val = internalMatrix[i, j];
                    answer += val + " ";
                }
                answer += "\n";
            }
            return answer;
        }
    }*/
}
