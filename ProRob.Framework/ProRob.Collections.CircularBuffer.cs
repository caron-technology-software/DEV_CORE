using System;
using System.Collections;
using System.Collections.Generic;

namespace ProRob.Collections
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private readonly T[] buffer;

        private int start;

        private int end;

        public int Size { get; private set; }

        public CircularBuffer(int capacity)
            : this(capacity, new T[] { })
        {
            // --
        }

        public CircularBuffer(int capacity, T[] items)
        {
            if (capacity < 1)
            {
                throw new ArgumentException(
                    "Circular buffer cannot have negative or zero capacity.", nameof(capacity));
            }
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }
            if (items.Length > capacity)
            {
                throw new ArgumentException(
                    "Too many items to fit circular buffer", nameof(items));
            }

            buffer = new T[capacity];

            Array.Copy(items, buffer, items.Length);
            Size = items.Length;

            start = 0;
            end = Size == capacity ? 0 : Size;
        }

        public int Capacity => buffer.Length;
        public bool IsFull => Size == Capacity;

        public bool IsEmpty => Size == 0;

        public T Front()
        {
            ThrowIfEmpty();
            return buffer[start];
        }

        public T Back()
        {
            ThrowIfEmpty();
            return buffer[(end != 0 ? end : Capacity) - 1];
        }

        public T this[int index]
        {
            get
            {
                if (IsEmpty)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer is empty", index));
                }
                if (index >= Size)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer size is {1}", index, Size));
                }
                int actualIndex = InternalIndex(index);
                return buffer[actualIndex];
            }
            set
            {
                if (IsEmpty)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer is empty", index));
                }
                if (index >= Size)
                {
                    throw new IndexOutOfRangeException(string.Format("Cannot access index {0}. Buffer size is {1}", index, Size));
                }
                int actualIndex = InternalIndex(index);
                buffer[actualIndex] = value;
            }
        }

        public void PushBack(T item)
        {
            if (IsFull)
            {
                buffer[end] = item;
                Increment(ref end);
                start = end;
            }
            else
            {
                buffer[end] = item;
                Increment(ref end);
                ++Size;
            }
        }

        public void PushFront(T item)
        {
            if (IsFull)
            {
                Decrement(ref start);
                end = start;
                buffer[start] = item;
            }
            else
            {
                Decrement(ref start);
                buffer[start] = item;
                ++Size;
            }
        }

        public void PopBack()
        {
            ThrowIfEmpty("Cannot take elements from an empty buffer.");
            Decrement(ref end);
            buffer[end] = default(T);
            --Size;
        }

        public void PopFront()
        {
            ThrowIfEmpty("Cannot take elements from an empty buffer.");
            buffer[start] = default(T);
            Increment(ref start);
            --Size;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[Size];
            int newArrayOffset = 0;
            var segments = ToArraySegments();
            foreach (ArraySegment<T> segment in segments)
            {
                Array.Copy(segment.Array, segment.Offset, newArray, newArrayOffset, segment.Count);
                newArrayOffset += segment.Count;
            }
            return newArray;
        }

        private IList<ArraySegment<T>> ToArraySegments()
        {
            return new[] { ArrayOne(), ArrayTwo() };
        }

        public IEnumerator<T> GetEnumerator()
        {
            var segments = ToArraySegments();
            foreach (ArraySegment<T> segment in segments)
            {
                for (int i = 0; i < segment.Count; i++)
                {
                    yield return segment.Array[segment.Offset + i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        private void ThrowIfEmpty(string message = "Cannot access an empty buffer.")
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException(message);
            }
        }

        private void Increment(ref int index)
        {
            if (++index == Capacity)
            {
                index = 0;
            }
        }

        private void Decrement(ref int index)
        {
            if (index == 0)
            {
                index = Capacity;
            }
            index--;
        }

        private int InternalIndex(int index)
        {
            return start + (index < (Capacity - start) ? index : index - Capacity);
        }

        // doing ArrayOne and ArrayTwo methods returning ArraySegment<T> as seen here: 
        // http://www.boost.org/doc/libs/1_37_0/libs/circular_buffer/doc/circular_buffer.html#classboost_1_1circular__buffer_1957cccdcb0c4ef7d80a34a990065818d
        // http://www.boost.org/doc/libs/1_37_0/libs/circular_buffer/doc/circular_buffer.html#classboost_1_1circular__buffer_1f5081a54afbc2dfc1a7fb20329df7d5b
        // should help a lot with the code.

        // The array is composed by at most two non-contiguous segments, 
        // the next two methods allow easy access to those.
        private ArraySegment<T> ArrayOne()
        {
            if (IsEmpty)
            {
                return new ArraySegment<T>(new T[0]);
            }
            else if (start < end)
            {
                return new ArraySegment<T>(buffer, start, end - start);
            }
            else
            {
                return new ArraySegment<T>(buffer, start, buffer.Length - start);
            }
        }

        private ArraySegment<T> ArrayTwo()
        {
            if (IsEmpty)
            {
                return new ArraySegment<T>(new T[0]);
            }
            else if (start < end)
            {
                return new ArraySegment<T>(buffer, end, 0);
            }
            else
            {
                return new ArraySegment<T>(buffer, 0, end);
            }
        }
    }
}