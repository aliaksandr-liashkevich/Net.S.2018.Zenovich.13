using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.S._2018.Zenovich._13.Matrix
{
    public class ChangedMatrixEventArgs<T> : EventArgs
    {
        public ChangedMatrixEventArgs(int rowIndex, int columnIndex,
            T oldValue, T newValue)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public int RowIndex { get; protected set; }

        public int ColumnIndex { get; protected set; }

        public T OldValue { get; protected set; }

        public T NewValue { get; protected set; }
    }
}
