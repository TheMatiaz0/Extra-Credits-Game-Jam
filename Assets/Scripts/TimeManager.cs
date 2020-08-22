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

	[SerializeField]
	private uint minutesPerTimeSpan = 10;

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

	public bool IsSleeping { get; set; }


	private Cint _CurrentDay;

	protected void Start()
	{
		StartNewDay();
	}

	public void SkipDay ()
	{
		IsSleeping = true;
		UIManager.Instance.OpenResults();
	}

	public void StartNewDay ()
	{
		IsSleeping = false;
		CurrentDay++;
		CurrentTime = new TimeSpan(6, 0, 0);
		StartCoroutine(TimeCount());
	}

	private IEnumerator TimeCount()
	{
		while (true)
		{
			yield return Async.Wait(inGameTimeSpan.TotalSeconds);
			CurrentTime = new TimeSpan(CurrentTime.Hours, CurrentTime.Minutes + (int)minutesPerTimeSpan, CurrentTime.Seconds);

			if (IsSleeping)
			{
				StopAllCoroutines();
			}

			if (CurrentTime.Days == 1)
			{
				StartNewDay();
				GameManager.Instance.HealthSys.Health.TakeValue(GameManager.Instance.HealthSys.Health.Max, "Death without Caution");
			}
		}
	}
}
