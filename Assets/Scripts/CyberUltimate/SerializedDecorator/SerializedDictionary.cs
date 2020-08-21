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
    public class SerializedDictionary<TKey,TValue>:ISerializationCallbackReceiver,IDictionary<TKey,TValue>, 
        ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable,
        IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary
    {
        public Dictionary<TKey, TValue> BaseDictionary { get; } = new Dictionary<TKey, TValue>();

        public ICollection<TKey> Keys => BaseDictionary.Keys;

        public ICollection<TValue> Values => BaseDictionary.Values;

        public int Count => BaseDictionary.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => ((IDictionary)BaseDictionary).IsFixedSize;

        ICollection IDictionary.Keys => ((IDictionary)BaseDictionary).Keys;

        ICollection IDictionary.Values => ((IDictionary)BaseDictionary).Values;

        public bool IsSynchronized => ((ICollection)BaseDictionary).IsSynchronized;

        public object SyncRoot => ((ICollection)BaseDictionary).SyncRoot;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => ((IReadOnlyDictionary<TKey, TValue>)BaseDictionary).Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => ((IReadOnlyDictionary<TKey, TValue>)BaseDictionary).Values;

        public object this[object key] { get => ((IDictionary)BaseDictionary)[key]; set => ((IDictionary)BaseDictionary)[key] = value; }
        public TValue this[TKey key]
        {
            get => BaseDictionary[key];
            set => BaseDictionary[key] = value;
        }

        [SerializeField]
        private  List<TKey> keys = new List<TKey>();
        [SerializeField]
        private List<TValue> values = new List<TValue>();
       

        public void Add(TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>)BaseDictionary).Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return ((IDictionary<TKey, TValue>)BaseDictionary).ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            return ((IDictionary<TKey, TValue>)BaseDictionary).Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return ((IDictionary<TKey, TValue>)BaseDictionary).TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)BaseDictionary).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)BaseDictionary).Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)BaseDictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)BaseDictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)BaseDictionary).Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)BaseDictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)BaseDictionary).GetEnumerator();
        }

        public void Add(object key, object value)
        {
            ((IDictionary)BaseDictionary).Add(key, value);
        }

        public bool Contains(object key)
        {
            return ((IDictionary)BaseDictionary).Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)BaseDictionary).GetEnumerator();
        }

        public void Remove(object key)
        {
            ((IDictionary)BaseDictionary).Remove(key);
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)BaseDictionary).CopyTo(array, index);
        }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            BaseDictionary.Clear();
            BaseDictionary.AddRange(keys.Zip(values, (key, value) => new KeyValuePair<TKey, TValue>(key, value)).DistinctBy(item => item.Key));
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            foreach (var item in BaseDictionary)
            {
                int index = keys.FindIndex(0, element => item.Key.Equals(element));
                if (index != -1)
                    values[index] = item.Value;
                else
                {
                    values.Add(item.Value);
                    keys.Add(item.Key);
                }
            }
        }
    }
}
