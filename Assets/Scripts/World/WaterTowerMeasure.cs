using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate;
using Cyberultimate.Unity;

public class WaterTowerMeasure : MonoSingleton<WaterTowerMeasure>
{
    public LockValue<uint> losedWater = new LockValue<uint>(15, 0, 0);
    public int minNeedleRot = 215;
    public int maxNeedleRot = 510;

    [SerializeField]
    private Transform needle;

    private float oneMoveRot;

    private void Start()
    {
        TimeManager.Instance.OnCurrentDayChange += OnDayChange;
        oneMoveRot= (maxNeedleRot - minNeedleRot)/ losedWater.Max;
        needle.localEulerAngles = new Vector3(0, minNeedleRot,0);
    }



    private void OnDayChange(object sender, SimpleArgs<Cint> e)
    {
        losedWater.GiveValue(1);
        needle.localEulerAngles = new Vector3(0, oneMoveRot * losedWater.Value, 0);
        //change model/ rotate so it shows 1 less
    }
}
