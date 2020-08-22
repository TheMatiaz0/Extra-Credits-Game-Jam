using Cyberultimate;
using Cyberultimate.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
	[SerializeField]
	private SerializedTimeSpan inGameTimeSpan;

	public TimeSpan CurrentTime { get; private set; }

	public Cint CurrentDay { get; private set; }

	protected void Start()
	{
		CurrentTime = new TimeSpan(20, 0, 0);
		StartCoroutine(TimeCount());
	}


	private IEnumerator TimeCount()
	{
		while (true)
		{
			yield return Async.Wait(inGameTimeSpan.TotalSeconds);
			CurrentTime = new TimeSpan(CurrentTime.Hours, CurrentTime.Minutes + 10, CurrentTime.Seconds);

			if (CurrentTime.Days == 1)
			{
				CurrentDay++;
				UnityEngine.Debug.Log(CurrentDay);
			}
		}
	}
}
