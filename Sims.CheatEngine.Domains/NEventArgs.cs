using System;

namespace Sims.CheatEngine.Domains
{
    public class NEventArgs<T> : EventArgs
    {
        public T Item { get; }

        public NEventArgs(T item)
        { 
            Item = item;
        }
    }
}