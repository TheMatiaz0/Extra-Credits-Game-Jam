using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using System.Collections;
namespace Cyberultimate.Unity
{
    [Serializable]
    public class ReorderableArray<T> : IList<T>, IList
    {
        [SerializeField]
        private T[] _Array = new T[0];

        public T this[int index] { get => ((IList<T>)_Array)[index]; set => ((IList<T>)_Array)[index] = value; }

        public T[] BaseArray
        {
            get => _Array;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
            }
        }

        public int Count => ((ICollection<T>)_Array).Count;
        public bool Contains(T item)
        {
            return ((ICollection<T>)_Array).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_Array).CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_Array).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)_Array).IndexOf(item);
        }
        bool ICollection<T>.IsReadOnly => ((ICollection<T>)_Array).IsReadOnly;

        bool IList.IsFixedSize => ((IList)_Array).IsFixedSize;

        bool IList.IsReadOnly => ((IList)_Array).IsFixedSize;

        bool ICollection.IsSynchronized => ((IList)_Array).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection)_Array).SyncRoot;

        object IList.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void ICollection<T>.Add(T item)
        {
            ((ICollection<T>)_Array).Add(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            ((IList<T>)_Array).Insert(index, item);
        }

        bool ICollection<T>.Remove(T item)
        {
            return ((ICollection<T>)_Array).Remove(item);
        }

        void IList<T>.RemoveAt(int index)
        {
            ((IList<T>)_Array).RemoveAt(index);
        }
        void ICollection<T>.Clear()
        {
            ((ICollection<T>)_Array).Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Array.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return ((IList)_Array).Add(value);
        }

        void IList.Clear()
        {
            ((IList)_Array).Clear();
        }

        bool IList.Contains(object value)
        {
            return ((IList)_Array).Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return ((IList)_Array).IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            ((IList)_Array).Insert(index, value);
        }

        void IList.Remove(object value)
        {
            ((IList)_Array).Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            ((IList)_Array).Remove(index);
        }

        void ICollection.CopyTo(Array array, int index)
        {

            ((IList)_Array).CopyTo(array, index);
        }
    }
}
