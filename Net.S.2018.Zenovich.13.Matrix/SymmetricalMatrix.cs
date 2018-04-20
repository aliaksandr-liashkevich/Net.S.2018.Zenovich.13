using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.S._2018.Zenovich._13.Matrix
{
    public class SymmetricalMatrix<T> : SquareMatrix<T>
    {
        public SymmetricalMatrix(int length, Func<T, T, T> sumFunc) 
            : base(length, sumFunc)
        {
        }

        public SymmetricalMatrix(T[] array, int length, Func<T, T, T> sumFunc) 
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            int jaggedArrayLength = GetJaggedArrayLength(length);

            if (jaggedArrayLength != array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(array));
            }

            RowLength = length;
            ColumnLength = length;
            matrix = new T[length, length];

            int index = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = i; j < length; j++)
                {
                    matrix[i, j] = array[index];

                    if (i != j)
                    {
                        matrix[j, i] = array[index];
                    }

                    index++;
                }
            }

            this.sumFunc = sumFunc;
        }

        public override T this[int RowIndex, int ColumnIndex]
        {
            get { return base[RowIndex, ColumnIndex]; }
            set
            {
                base[RowIndex, ColumnIndex] = value;

                if (RowIndex != ColumnIndex)
                {
                    var eventArgs = CreateChangeMatrixEventArgs(ColumnIndex, RowIndex, value);

                    matrix[ColumnIndex, RowIndex] = value;

                    ChangeMatrixEventInvoke(eventArgs);
                }
            }
        }

        private int GetJaggedArrayLength(int length)
        {
            int result = 0;

            for (int i = length; i > 0; i--)
            {
                result = result + i;
            }

            return result;
        }
    }
}
