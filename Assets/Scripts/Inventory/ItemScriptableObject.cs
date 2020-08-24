using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
	public Sprite icon;
    public float useTakeLessTime = 0;
    public bool oneTimeLoot = true;

	[SerializeReference]
	[EditableSerializeReference(typeof(ItemLogic))]
	private ItemLogic actionForItem;

	public ItemLogic ActionForItem => actionForItem;
}
