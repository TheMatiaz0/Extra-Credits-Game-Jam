using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;
using Cyberultimate;

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

    public void ResetGarbage(object sender, SimpleArgs<Cint> e)
    {
        foreach(Garbage g in transform.GetComponentsInChildren<Garbage>())
        {
            g.garbageUsed = false;
            g.interactionEnabled = true;
        }
    }

    private void Start()
    {
        TimeManager.Instance.OnCurrentDayChange += ResetGarbage;
    }
}
