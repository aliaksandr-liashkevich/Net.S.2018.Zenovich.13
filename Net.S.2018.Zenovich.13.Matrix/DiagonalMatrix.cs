using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.S._2018.Zenovich._13.Matrix
{
    public class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(int length, Func<T, T, T> sumFunc) 
            : base(length, sumFunc)
        {
        }

        public DiagonalMatrix(T[] array, Func<T, T, T> sumFunc) 
        {
            if (ReferenceEquals(array, null))
            {
                throw new ArgumentNullException(nameof(array));
            }

            int length = array.Length;
            RowLength = length;
            ColumnLength = length;

            matrix = new T[length, length];

            for (int i = 0; i < length; i++)
            {
                matrix[i, i] = array[i];
            }

            this.sumFunc = sumFunc;
        }

        public override T this[int RowIndex, int ColumnIndex]
        {
            get
            {
                base.CheckIndexRange(RowIndex, ColumnIndex);
                
                if (RowIndex != ColumnIndex)
                {
                    return default(T);
                }

                return matrix[RowIndex, ColumnIndex];
            }
            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException();
                }

                this.CheckIndexRange(RowIndex, ColumnIndex);

                var eventArgs = CreateChangeMatrixEventArgs(RowIndex, ColumnIndex, value);

                matrix[RowIndex, ColumnIndex] = value;

                ChangeMatrixEventInvoke(eventArgs);
            }
        }

        protected override void CheckIndexRange(int RowIndex, int ColumnIndex)
        {
            base.CheckIndexRange(RowIndex, ColumnIndex);

            if (RowIndex != ColumnIndex)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
