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

	public TimeSpan CurrentTime 
	{
		get { return _CurrentTime; }
		private set { if (value == _CurrentTime) return; _CurrentTime = value; OnCurrentTimeChange(this, _CurrentTime); }
	}

	private TimeSpan _CurrentTime;

	public event EventHandler<SimpleArgs<TimeSpan>> OnCurrentTimeChange = delegate { };

	public Cint CurrentDay 
	{
		get { return _CurrentDay; }
		private set { if (value == _CurrentDay) return;  _CurrentDay = value; OnCurrentDayChange(this, _CurrentDay); } 
	}

	public event EventHandler<SimpleArgs<Cint>> OnCurrentDayChange = delegate { };


	private Cint _CurrentDay;

	protected void Start()
	{
		CurrentTime = new TimeSpan(8, 0, 0);
		StartCoroutine(TimeCount());
	}


	private IEnumerator TimeCount()
	{
		while (true)
		{
			yield return Async.Wait(inGameTimeSpan.TotalSeconds);
			CurrentTime = new TimeSpan(CurrentTime.Hours, CurrentTime.Minutes + 10, CurrentTime.Seconds);

			CurrentDay += (Cint)((uint)CurrentTime.Days);
			// UnityEngine.Debug.Log(CurrentDay);

			/*
			if (CurrentTime.Days == 1)
			{
				CurrentDay++;

			}
			*/
		}
	}
}
