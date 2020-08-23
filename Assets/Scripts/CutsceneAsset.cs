using Cyberultimate.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CutsceneAsset
{
	[TextArea]
	[SerializeField]
	private string sentence = null;
	public string Sentence => sentence;

	[SerializeField]
	private SerializedTimeSpan cooldownToNext;

	public TimeSpan CooldownToNext => cooldownToNext.TimeSpan;
}
