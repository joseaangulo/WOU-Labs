using System.ComponentModel.Design;

namespace Container.Lib;

public class Container<TE> : IContainer<TE>
{
    private TE[] _container;
    private int _count;
    private const int SIZE = 5;

    public Container()
    {
        _container = new TE[SIZE];
    }

    public void Insert(TE e)
    {
        if(e == null) throw new ArgumentNullException(nameof(e));
        if (_count < _container.Length)
        {
            _container[_count] = e;
            _count++;
        }
        else
        {
            throw new Exception("Attempting insertion on full container.");
        }
    }

    public void Delete(TE e)
    {
        if (_count == 0) throw new Exception("Attempting deletion on empty container.");
        if (e.GetType() != typeof(TE)) throw new Exception("Attempting exists on item of wrong type");
        if (!Exists(e)) throw new Exception("Attempting deletion on container without item.");
        for(int i = 0; i < _count; i++)
        {
            if (_container[i].Equals(e))
                _container[i] = _container[_count - 1];
            _count--;
        }

    }

    public bool Exists(TE e)
    {
        if(e == null) throw new Exception("Attempting exists on null.");
        if (e.GetType() != typeof(TE)) throw new Exception("Attempting exists on item of wrong type");
        if (_count == 0) return false;
        foreach (var item in _container)
        {
            if (item.Equals(e))
            {
                return true;
            }
        }
        
        return false;
    }
}