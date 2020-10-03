using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropPattern", menuName = "ScriptableObjects/DropPatterns", order = 1)]
public class DropPattern : ScriptableObject
{
	[SerializeField]
	private ReorderableArray<ItemScriptableObject> itemChanceDrops;
	public ItemScriptableObject[] ItemChanceDrops => itemChanceDrops.BaseArray;
}
