using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MapKit.Core
{
    [DebuggerDisplay("Count = {Count}")]
    public class ThemeNodeCollection<T> : IList<T> where T : ThemeNode
    {
        private ContainerNode _parent;
        private IList<T> _list;

        public event EventHandler<ItemEventArgs<T>> ItemAdded;
        public event EventHandler<ItemEventArgs<T>> ItemRemoved;

        internal ThemeNodeCollection(ContainerNode containerNode)
        {
            Debug.Assert(containerNode != null);
            _parent = containerNode;
            _list = new List<T>();
        }

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ValidateNew(item);
            _list.Insert(index, item);
            if (ItemAdded != null)
                ItemAdded(this, new ItemEventArgs<T>(item));
        }

        private void ValidateNew(T item)
        {
            if (item.Parent != null) throw new InvalidOperationException("Item already belong to another collection");
            item.Parent = _parent;
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            Invalidate(item);
            _list.RemoveAt(index);
            
            if (ItemRemoved != null)
                ItemRemoved(this, new ItemEventArgs<T>(item));
        }

        private void Invalidate(T item, bool notify = true)
        {
            Debug.Assert(item.Parent == _parent);
            item.Parent = null;
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set
            {
                ValidateNew(value);

                var item = _list[index];
                Invalidate(item);
                if (ItemRemoved != null)
                    ItemRemoved(this, new ItemEventArgs<T>(item));
                
                _list[index] = value;
                
                if (ItemAdded != null)
                    ItemAdded(this, new ItemEventArgs<T>(value));
            }
        }

        public void Add(T item)
        {
            ValidateNew(item);
            _list.Add(item);
            if (ItemAdded != null)
                ItemAdded(this, new ItemEventArgs<T>(item));
        }

        public void Clear()
        {
            var list = new List<T>(_list);
            foreach (var item in _list)
                Invalidate(item, false);
            _list.Clear();

            if (ItemRemoved != null)
                foreach (var item in list)
                    ItemRemoved(this, new ItemEventArgs<T>(item));
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get 
            { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            if (_list.Remove(item))
            {
                Invalidate(item);
                if (ItemRemoved != null)
                    ItemRemoved(this, new ItemEventArgs<T>(item));
                return true;
            }
            return false;
        }

        private bool Validate(T item)
        {
            return item.Parent == _parent;
        }

        [DebuggerStepThrough]
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
