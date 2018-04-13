using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.S._2018.Zenovich._13.Matrix
{
    public class SquareMatrix<T>
    {
        #region Public fields

        public event EventHandler<ChangedMatrixEventArgs<T>> ChangedMatrixEvent = delegate { };

        #endregion Public fields

        #region Protected fields

        protected T[,] matrix;

        protected Func<T, T, T> sumFunc;

        #endregion Protected fields

        #region Ctor

        public SquareMatrix(int length, Func<T,T,T> sumFunc)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            RowLength = length;
            ColumnLength = length;

            matrix = new T[RowLength, ColumnLength];
            this.sumFunc = sumFunc;
        }

        public SquareMatrix(T[] array, Func<T, T, T> sumFunc)
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (IsFullSquare(array.Length) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(array));
            }

            int length = (int)Math.Sqrt(array.Length);
            int arrayIndex = 0;

            for (int rowIndex = 0; rowIndex < length; rowIndex++)
            {
                for (int columIndex = 0; columIndex < length; columIndex++)
                {
                    matrix[rowIndex, columIndex] = array[arrayIndex];
                    arrayIndex++;
                }
            }

            this.sumFunc = sumFunc;
        }

        protected SquareMatrix()
        {
        }

        #endregion Ctor

        #region Properties

        public int RowLength { get; protected set; }

        public int ColumnLength { get; protected set; }

        public virtual T this[int RowIndex, int ColumnIndex]
        {
            get
            {
                CheckIndexRange(RowIndex, ColumnIndex);

                return matrix[RowIndex, ColumnIndex];
            }
            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException();
                }

                CheckIndexRange(RowIndex, ColumnIndex);

                var eventArgs = CreateChangeMatrixEventArgs(RowIndex, ColumnIndex, value);

                matrix[RowIndex, ColumnIndex] = value;

                ChangeMatrixEventInvoke(eventArgs);
            }
        }

        #endregion Properties

        #region Public methods

        public SquareMatrix<T> Sum(SquareMatrix<T> firstMatrix, SquareMatrix<T> secondMatrix)
        {
            if (ReferenceEquals(firstMatrix, null))
            {
                throw new ArgumentNullException(nameof(firstMatrix));
            }

            if (ReferenceEquals(secondMatrix, null))
            {
                throw new ArgumentNullException(nameof(secondMatrix));
            }

            SquareMatrix<T> result = new SquareMatrix<T>();

            result.matrix = SumMatrix(firstMatrix.matrix, secondMatrix.matrix);

            return result;
        }

        #endregion Public methods

        #region Protected methods

        protected T[,] SumMatrix(T[,] firstMatrix, T[,] secondMatrix)
        {
            if (firstMatrix.Length != secondMatrix.Length)
            {
                throw new ArgumentException("Indexes are not even.");
            }

            int rowLength = firstMatrix.GetLength(0);
            int columnLength = firstMatrix.GetLength(1);

            T[,] result = new T[rowLength, columnLength];

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; i < columnLength; j++)
                {
                    result[i, j] = sumFunc(firstMatrix[i, j], secondMatrix[i, j]);
                }
            }

            return result;
        }

        protected void ChangeMatrixEventInvoke(ChangedMatrixEventArgs<T> eventArgs)
        {
            ChangedMatrixEvent.Invoke(this, eventArgs);
        }

        protected virtual void CheckIndexRange(int RowIndex, int ColumnIndex)
        {
            if (RowIndex < 0 || RowIndex >= RowLength)
            {
                throw new ArgumentOutOfRangeException(nameof(RowIndex));
            }

            if (ColumnIndex < 0 || ColumnIndex >= ColumnLength)
            {
                throw new ArgumentOutOfRangeException(nameof(ColumnIndex));
            }
        }

        protected ChangedMatrixEventArgs<T> CreateChangeMatrixEventArgs(int RowIndex, int ColumnIndex, T oldValue)
        {
            T newValue = matrix[RowIndex, ColumnIndex];

            return new ChangedMatrixEventArgs<T>(RowIndex, ColumnIndex,
                oldValue, newValue);
        }

        protected bool IsFullSquare(int number)
        {
            int sqrtNumber = (int)Math.Sqrt(number);

            if (number * number == sqrtNumber)
            {
                return true;
            }

            return false;
        }

        #endregion Protected methods

    }
}
