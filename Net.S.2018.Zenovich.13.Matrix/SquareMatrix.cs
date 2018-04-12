using System;

namespace Net.S._2018.Zenovich._13.Matrix
{
    public class SquareMatrix<T>
    {
        #region Public fields

        public event EventHandler<ChangedMatrixEventArgs<T>> ChangedMatrixEvent = delegate { };

        #endregion Public fields

        #region Private fields

        private T[,] matrix;

        #endregion Private fields

        public SquareMatrix(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            RowLength = length;
            ColumnLength = length;

            matrix = new T[RowLength, ColumnLength];
        }   

        public SquareMatrix(T[] array)
        {
            if (IsFullSquare(array.Length) == false)
            {
                throw new ArgumentOutOfRangeException(nameof(array));
            }

            int length = (int) Math.Sqrt(array.Length);
            int arrayIndex = 0;

            for (int rowIndex = 0; rowIndex < length; rowIndex++)
            {
                for (int columIndex = 0; columIndex < length; columIndex++)
                {
                    matrix[rowIndex, columIndex] = array[arrayIndex];
                    arrayIndex++;
                }
            }
        }

        #region Properties

        public int RowLength { get; protected set; }

        public int ColumnLength { get; protected set; }

        public virtual T this[int RowIndex, int ColumnIndex]
        {
            get
            {
                if (RowIndex < 0 || RowIndex >= RowLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(RowIndex));
                }

                if (ColumnIndex < 0 || ColumnIndex >= ColumnLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(ColumnIndex));
                }

                return matrix[RowIndex, ColumnIndex];
            }
            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException();
                }

                if (RowIndex < 0 || RowIndex >= RowLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(RowIndex));
                }

                if (ColumnIndex < 0 || ColumnIndex >= ColumnLength)
                {
                    throw new ArgumentOutOfRangeException(nameof(ColumnIndex));
                }

                var eventArgs = CreateChangeMatrixEventArgs(RowIndex, ColumnIndex, value);

                matrix[RowIndex, ColumnIndex] = value;

                ChangedMatrixEvent.Invoke(this, eventArgs);
            }
        }

        #endregion Properties

        #region Private methods

        private ChangedMatrixEventArgs<T> CreateChangeMatrixEventArgs(int RowIndex, int ColumnIndex, T oldValue)
        {
            T newValue = matrix[RowIndex, ColumnIndex];

            return new ChangedMatrixEventArgs<T>(RowIndex, ColumnIndex, 
                oldValue, newValue);
        }

        private bool IsFullSquare(int number)
        {
            int sqrtNumber = (int) Math.Sqrt(number);

            if (number * number == sqrtNumber)
            {
                return true;
            }

            return false;
        }

        #endregion Private methods

    }
}
