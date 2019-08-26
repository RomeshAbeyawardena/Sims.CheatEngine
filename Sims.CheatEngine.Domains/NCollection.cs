using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sims.CheatEngine.Domains
{
    public class NCollection<T> : ICollection<T>
    {
        private readonly Collection<T> _collection;
        public event EventHandler<NEventArgs<T>> ItemAdded;
        public event EventHandler<NEventArgs<T>> ItemRemoved;

        public T this[int index] => _collection[index];

        public NCollection()
        {
            _collection = new Collection<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public void Add(T item)
        {
            ItemAdded?.Invoke(this, new NEventArgs<T>(item));
            _collection.Add(item);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public bool Contains(T item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _collection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            ItemRemoved?.Invoke(this, new NEventArgs<T>(item));
            return _collection.Remove(item);
        }

        public int Count => _collection.Count;
        public bool IsReadOnly => false;
    }
}