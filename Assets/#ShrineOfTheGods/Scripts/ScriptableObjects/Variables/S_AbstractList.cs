using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_AbstractList<T> : ScriptableObject
{

    public List<T> items = new List<T>();

    public void Add(T item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
        else
            Debug.Log("item is already in the list");
    }

    public void Remove(T item)
    {
        if(items.Contains(item))
            items.Remove(item);
    }

    public T GetRandom()
    {
        if (items.Count == 0)
            return default(T);
        else if (items.Count == 1)
            return items[0];
        else
        {
            int r = Random.Range(0, items.Count);
            return items[r];
        }
    }

}
