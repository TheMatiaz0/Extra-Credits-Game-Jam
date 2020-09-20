using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TypeReferences;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
	public Sprite icon;
    public float useTakeLessTime = 0;
    public bool oneTimeLoot = true;
	public bool useable = false;

	public bool fillable = false;
	public bool destructible = false;
	public uint maxDurability;

	[ClassExtends(typeof(ItemLogic))]
	public ClassTypeReference itemAction;
	
}
