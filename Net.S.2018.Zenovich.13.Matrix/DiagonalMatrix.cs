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

            for (int i = 0; i < length; i++)
            {
                matrix[i, i] = array[i];
            }

            this.sumFunc = sumFunc;
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
