﻿
using System;
using Cyberultimate;
using TypeReferences;
using UnityEngine;

public class Item
{
    public string Name { get; private set; }
    
    public Sprite Icon { get; private set; }
    public float UseTakeLessTime { get; private set; } = 0;
    public bool OneTimeLoot { get; private set; } = true;
    public bool Useable { get; private set; } = false;

    public bool Fillable { get; private set; } = false;

    public LockValue<uint> FillAmount { get; private set; } = new LockValue<uint>(100, 0, 0);

    public ItemLogic Logic { get; private set; }

    public Item(ItemScriptableObject baseObj)
    {
        if(baseObj.itemAction != null && baseObj != null)
            Logic = Activator.CreateInstance(baseObj.itemAction) as ItemLogic;

        Name = baseObj.name;
        Icon = baseObj.icon;
        UseTakeLessTime = baseObj.useTakeLessTime;
        OneTimeLoot = baseObj.oneTimeLoot;
        Useable = baseObj.useable;
        Fillable = baseObj.fillable;
    }
}
