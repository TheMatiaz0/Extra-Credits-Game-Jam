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

	[ClassExtends(typeof(ItemLogic))]
	public ClassTypeReference itemAction;
}
