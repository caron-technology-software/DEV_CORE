using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[IMPROVE]

namespace ProRob.Extensions.Collections
{
    /// <summary>
    /// Defines circular queue methods.
    /// </summary>
    public interface ICircularQueue<T>
    {
        /// <summary>
        /// Enqueues <paramref name="x"/> into queue.
        /// </summary>
        void Enqueue(T x);

        /// <summary>
        /// Dequeues head from queue.
        /// </summary>
        T Dequeue();

        /// <summary>
        /// Peeks head of queue.
        /// </summary>
        T Peek();

        /// <summary>
        /// Peeks tail of queue.
        /// </summary>
        T PeekTail();

        /// <summary>
        /// Returns true if queue is empty.
        /// </summary>
        bool IsEmpty();

        /// <summary>
        /// Returns count of queue.
        /// </summary>
        int Count();

        /// <summary>
        /// Returns capacity of queue.
        /// </summary>
        int Capacity();

        /// <summary>
        /// Clears queue.
        /// </summary>
        void Clear();
    }
}
