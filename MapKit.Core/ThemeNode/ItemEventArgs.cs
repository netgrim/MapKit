using System;

namespace MapKit.Core
{
    public class ItemEventArgs<T> : EventArgs
    {
        public ItemEventArgs(T item)
        {
            Item = item;
        }

        public T Item { get; set; }
    }
}
