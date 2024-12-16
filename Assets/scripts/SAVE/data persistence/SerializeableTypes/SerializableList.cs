using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableList<TValue> : List<TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<TValue> values = new List<TValue>();

    //save the dictionary to lists
    public void OnBeforeSerialize()
    {
        values.Clear();
        foreach (TValue value in this)
        {
            values.Add(value);
        }
/*        for (int i = 0; i < values.Count; i++)
        {
            values[i] = this[i];
        }*/
    }
    //load the dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();
/*        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys (" + keys.Count + ") does not match the number of values (" + values.Count + ") which indicates that something went wrong.");
        }*/
        for (int i = 0; i < values.Count; i++)
        {
            this.Add(values[i]);
        }
    }
}
