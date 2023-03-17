namespace CircularQueue.Lib;

public class CircularQueue<T> : ICircularQueue<T>
{
    private T[] _queue;
    private  int _count = 0;
    private int _front = 0;
    private int _back = 0;

    public CircularQueue(int maxSize)
    {
        _queue = new T[maxSize];
    }
    public void Enqueue(T item)
    {
        if (IsFull) throw new Exception("The queue is full!");
        _queue[_back] = item;

        _back = (_back + 1) % _queue.Length;

        ++_count;
    }

    public T Dequeue()
    {
        if (IsEmpty) throw new Exception("The queue is empty!");
        var oldFront = _queue[_front];
        _front = (_front + 1) % _queue.Length;
        --_count;
        return oldFront;
    }

    public T Front()
    {
        if (IsEmpty) throw new Exception("The queue is empty!");
        return _queue[_front];
    }

    public T Back()
    {
        if (IsEmpty) throw new Exception("The queue is empty!");
        return _queue [_back - 1];
    }

    public void Resize(int newSize)
    {
        if (newSize == _queue.Length) throw new Exception("Resize of same size!");

        if (newSize < _queue.Length)
        {
            T[] temp = new T[newSize];
            
            for (int i = 0; i < newSize; ++i)
            {
                temp[i] = Dequeue();
            }

            _queue = temp;
            temp = null;
            _count = newSize;
        }
        else
        {
            T[] temp = new T[_queue.Length];
            int oldCount = _count;

            for (int i = 0; i < _count; ++i)
            {
                temp[i] = Dequeue();
            }

            _queue = new T[newSize];

            for (int i = 0; i < oldCount; i++)
            {
                Enqueue(temp[i]);
            }
        }

    }

    public bool IsEmpty => _count == 0;
    public bool IsFull => _count == _queue.Length;
    public int Count => _count;
}