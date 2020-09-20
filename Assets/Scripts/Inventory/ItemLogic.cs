using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;

[Serializable]
public abstract class ItemLogic
{
    public bool removeOnUse;

    protected Item item;
    
    public ItemLogic(Item item)
    {
        this.item = item;
    }

    public void Use()
    {
        OnUse();
        item.Durability.TakeValue(1);
    }
    
    protected abstract void OnUse();
}
