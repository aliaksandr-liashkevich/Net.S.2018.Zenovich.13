using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.S._2018.Zenovich._13.Queue
{
    public class UserQueue<T> : IEnumerable<T>
    {
        #region Private fields

        private T[] _queue;

        private int _head;

        private int _tail;

        private int _count;

        #endregion Private fields

        public UserQueue()
             : this(50)
        {
        }

        public UserQueue(int capacity)
        {
            _queue = new T[capacity];
        }

        public int Count => _count;

        public void Enqueue(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (_count == _queue.Length)
            {
                this.Resize();
            }

            if (_tail == _queue.Length - 1)
            {
                _tail = 0;
            }

            _queue[_tail] = item;
            _tail++;
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Can't dequeue from empty UserQueue.");
            }

            T result = _queue[_head];

            _head++;
            _count--;

            if (_head == _queue.Length)
            {
                _head = 0;
            }

            return result;
        }


        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private T this[int index]
        {
            get { return _queue[index]; }
        }

        private void Resize()
        {
            int newLength = _queue.Length * 2;
            T[] newArray = new T[newLength];

            if (_head < _tail)
            {
                Array.Copy(_queue, _head, newArray, 0, _tail - _head);
            }
            else
            {
                Array.Copy(_queue, _head, newArray, 0, _queue.Length - _head);
                Array.Copy(_queue, 0, newArray, _queue.Length - _head + 1, _tail);
            }

            _head = 0;
            _tail = _count;

            _queue = newArray;
        }

        private struct UserQueueEnumerator: IEnumerator<T>
        {
            private readonly UserQueue<T> _userQueue;
            private int _currentIndex;

            public UserQueueEnumerator(UserQueue<T> userQueue)
            {
                _userQueue = userQueue;
                _currentIndex = -1;
            }


            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                _currentIndex++;

                if (_currentIndex < _userQueue.Count)
                {
                    return true;
                }

                Reset();
                return false;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public T Current
            {
                get
                {
                    if (_currentIndex == -1 || _currentIndex == _userQueue.Count)
                    {
                        throw new InvalidOperationException();
                    }

                    return _userQueue[_currentIndex];
                }
            }

            object IEnumerator.Current => Current;
        }
    }
}
