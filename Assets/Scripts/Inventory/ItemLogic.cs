using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;

[Serializable]
public abstract class ItemLogic
{
    public bool removeOnUse;

    public abstract void Do();
}
