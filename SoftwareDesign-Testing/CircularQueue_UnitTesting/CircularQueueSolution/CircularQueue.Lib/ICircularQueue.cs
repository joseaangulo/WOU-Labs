namespace CircularQueue.Lib
{
    /// <summary>
    /// A CircularQueue is a fixed-size FIFO data structure. 
    /// </summary>
    /// <remarks>
    /// The CircularQueue must know its size up front. Must support
    /// standard FIFO operations and must wrap-around to utilize the space
    /// of the underlying structure.
    /// </remarks>
    public interface ICircularQueue<T>
    {
        /// <summary>
        /// Enqueue inserts an item into the front of the queue.
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. A queue has a size with front and back
        /// 2. Verified that the queue has capacity
        /// Post-conditions:
        /// 1. Count is incremented by one
        /// 2. Front and back are affected if data is present
        /// 3. IsEmpty property will return false
        /// Invariants:
        /// 1. Queue is full - should throw an exception
        /// 2. Enqueuing requires wrap-around to the front of the structure
        /// as needed
        /// </remarks>
        /// <param name="item">The item to enqueue</param>
        void Enqueue(T item);

        /// <summary>
        /// Dequeue removes an item from the front of the queue.
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. The list is not empty
        /// 2. The container size of the list is greater than 0
        /// Post-conditions:
        /// 1. Count is decremented by one
        /// 2. An item is returned
        /// 3. Front is updated to new value for front
        /// Invariants:
        /// 1. Throw an exception if the list is empty
        /// </remarks>
        /// <returns>The item at the front</returns>
        T Dequeue();

        /// <summary>
        /// Returns an item from the front of the queue.
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. The list is not empty
        /// 2. The container size of the list is greater than 0
        /// Post-conditions:
        /// 1. An item is returned
        /// Invariants:
        /// 1. Throw an exception if the list is empty
        /// </remarks>
        /// <returns>The item at the front</returns>
        T Front();

        /// <summary>
        /// Returns an item from the back of the queue.
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. The list is not empty
        /// 2. The container size of the list is greater than 0
        /// Post-conditions:
        /// 1. An item is returned
        /// Invariants:
        /// 1. Throw an exception if the list is empty
        /// </remarks>
        /// <returns>The item at the back</returns>
        T Back();

        /// Resize the max size of the queue.
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. The queue is full
        /// Post-conditions:
        /// 1. newSize is the new maxSize
        /// 2. Data retains its order
        /// Invariants:
        /// 1. If the new size is smaller than the weight of the queue
        ///    data truncation will occur
        /// 2. If newSize == current size, the operation doesn't make sense
        /// </remarks>
        /// <param name="newSize">The new desired size of the queue</param>
        void Resize(int newSize);

        /// Returns whether the queue is empty
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. A queue exists
        /// Post-conditions:
        /// 1. The value is true if it's empty, false otherwise
        /// </remarks>
        bool IsEmpty { get; }

        /// Returns whether the queue has capacity
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. A queue exists
        /// Post-conditions:
        /// 1. The value is true if at capacity, false otherwise
        /// </remarks>
        bool IsFull { get; }

        /// Returns the current size of the queue (the number of items in the queue)
        /// </summary>
        /// <remarks>
        /// Pre-conditions:
        /// 1. Front and Back are tracked
        /// 2. Tracking the number of elements
        /// Post-conditions:
        /// 1. A valid integer integer is returns
        /// </remarks>
        /// <returns>The size of the queue</returns>
        int Count { get; }


    }
}