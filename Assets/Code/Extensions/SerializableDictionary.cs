using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Serialization;

public class SerializableDictionary { }

[Serializable]
public class SerializableDictionary<TKey, TValue> : SerializableDictionary, ISerializationCallbackReceiver, IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
{

    [SerializeField]
    private List<SerializableKeyValuePair> list = new List<SerializableKeyValuePair>();

    [Serializable]
    public struct SerializableKeyValuePair
    {
        public TKey Key;
        public TValue Value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public void SetValue(TValue value)
        {
            Value = value;
        }
    }


    private Dictionary<TKey, uint> KeyPositions => _keyPositions.Value;
    private Lazy<Dictionary<TKey, uint>> _keyPositions;

    public static Dictionary<TKey, TValue> ToDictionary(SerializableDictionary<TKey, TValue> serializableDictionary)
    {
        Dictionary<TKey, TValue> dictionary = new();

        foreach (SerializableKeyValuePair pair in serializableDictionary.list)
        {
            dictionary.Add(pair.Key, pair.Value);
        }

        return dictionary;
    }

    public SerializableDictionary(IDictionary<TKey, TValue> dictionary) : this()
    {
        foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
        {
            Add(kvp.Key, kvp.Value);
        }
    }

    public SerializableDictionary()
    {
        _keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);
    }

    private Dictionary<TKey, uint> MakeKeyPositions()
    {
        var numEntries = list.Count;
        var result = new Dictionary<TKey, uint>(numEntries);
        for (int i = 0; i < numEntries; i++)
            result[list[i].Key] = (uint)i;
        return result;
    }

    public void OnBeforeSerialize() { }
    public void OnAfterDeserialize()
    {
        // After deserialization, the key positions might be changed
        _keyPositions = new Lazy<Dictionary<TKey, uint>>(MakeKeyPositions);
    }

    #region IDictionary<TKey, TValue>
    public TValue this[TKey key]
    {
        get => list[(int)KeyPositions[key]].Value;
        set
        {
            if (KeyPositions.TryGetValue(key, out uint index))
            {
                var temp = list[(int)index];
                temp.SetValue(value);
                list[(int)index] = temp;
            }
            else
            {
                KeyPositions[key] = (uint)list.Count;
                list.Add(new SerializableKeyValuePair(key, value));
            }
        }
    }

    public ICollection<TKey> Keys => list.Select(tuple => tuple.Key).ToArray();
    public ICollection<TValue> Values => list.Select(tuple => tuple.Value).ToArray();

    public void Add(TKey key, TValue value)
    {
        if (KeyPositions.ContainsKey(key))
            throw new ArgumentException("An element with the same key already exists in the dictionary.");
        else
        {
            KeyPositions[key] = (uint)list.Count;
            list.Add(new SerializableKeyValuePair(key, value));
        }
    }
    
    public void AddRange(IDictionary<TKey, TValue> dictionary)
    {
        foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
        {
            Add(kvp.Key, kvp.Value);
        }
    }

    public bool ContainsKey(TKey key) => KeyPositions.ContainsKey(key);

    public bool Remove(TKey key)
    {
        if (!KeyPositions.TryGetValue(key, out uint index))
            return false;

        list.RemoveAt((int)index);

        _keyPositions = new Lazy<Dictionary<TKey, uint>>(() => list
            .Select((kvp, index) => new { kvp.Key, Index = (uint)index })
            .ToDictionary(indexedKvp => indexedKvp.Key, indexedKvp => indexedKvp.Index));

        return true;
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        if (KeyPositions.TryGetValue(key, out uint index))
        {
            value = list[(int)index].Value;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }
    #endregion


    #region ICollection <KeyValuePair<TKey, TValue>>
    public int Count => list.Count;
    public bool IsReadOnly => false;

    public void Add(KeyValuePair<TKey, TValue> kvp) => Add(kvp.Key, kvp.Value);

    public void Clear()
    {
        list.Clear();
        _keyPositions = new Lazy<Dictionary<TKey, uint>>(() => new Dictionary<TKey, uint>());
    }

    public bool Contains(KeyValuePair<TKey, TValue> kvp) => KeyPositions.ContainsKey(kvp.Key);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        var numKeys = list.Count;
        if (array.Length - arrayIndex < numKeys)
            throw new ArgumentException("arrayIndex");
        for (int i = 0; i < numKeys; i++, arrayIndex++)
        {
            var entry = list[i];
            array[arrayIndex] = new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
        }
    }

    public bool Remove(KeyValuePair<TKey, TValue> kvp) => Remove(kvp.Key);
    #endregion


    #region IEnumerable <KeyValuePair<TKey, TValue>>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return list.Select(ToKeyValuePair).GetEnumerator();

        KeyValuePair<TKey, TValue> ToKeyValuePair(SerializableKeyValuePair skvp)
        {
            return new KeyValuePair<TKey, TValue>(skvp.Key, skvp.Value);
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion


    #region IReadOnlyDictionary<TKey, TValue>
    IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => list.Select(tuple => tuple.Key).ToArray();
    IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => list.Select(tuple => tuple.Value).ToArray();
    #endregion

}