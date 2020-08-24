using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;

public class GarbageManager : MonoSingleton<GarbageManager>
{
    public SerializedDictionary<ItemScriptableObject, int> itemChanceDrops = new SerializedDictionary<ItemScriptableObject, int>();
    public List<ItemScriptableObject> itemDrops;

    public List<ItemScriptableObject> shovelEvolution;
    public List<ItemScriptableObject> bottleEvolution;

    public ItemScriptableObject firstItem;

    [HideInInspector]
    public bool firstItemUsed=false;
    [HideInInspector]
    public bool garbage1 = true;
}
