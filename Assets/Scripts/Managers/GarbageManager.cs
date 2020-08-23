using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;

public class GarbageManager : MonoSingleton<GarbageManager>
{
    public void AddItemToAll(ItemScriptableObject item)
    {
        foreach(Garbage child in transform.GetComponentsInChildren<Garbage>())
        {
            if (child.itemDrops.Contains(item)) break;
            child.itemDrops.Add(item);
        }
    }

    public void FirstItemUsedToAll()
    {
        foreach (Garbage child in transform.GetComponentsInChildren<Garbage>())
        {
            child.firstItemGet = true;
        }

    }
}
