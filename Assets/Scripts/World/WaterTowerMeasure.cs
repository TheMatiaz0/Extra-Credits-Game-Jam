using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;
using Cyberultimate.Unity;

public class WaterTowerMeasure : MonoSingleton<WaterTowerMeasure>
{
    public LockValue<uint> water = new LockValue<uint>(15, 0, 15);

    private void Start()
    {
        TimeManager.Instance.OnCurrentDayChange += OnDayChange;
    }



    private void OnDayChange(object sender, SimpleArgs<Cint> e)
    {
        water.TakeValue(1);
        //change model/ rotate so it shows 1 less
    }
}
