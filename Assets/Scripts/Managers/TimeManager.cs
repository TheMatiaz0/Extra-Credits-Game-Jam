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
	private uint minutesPerTimeSpan = 5;

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
		StartNewDay();
	}

	public void SkipDay ()
	{
		StopAllCoroutines();
		UIManager.Instance.OpenResults();
	}

	public void StartNewDay ()
	{
		GameManager.Instance.StaminaSys.Stamina.SetValue(GameManager.Instance.StaminaSys.Stamina.Max);
		
		CurrentDay++;
		CurrentTime = new TimeSpan(6, 0, 0);
		StartCoroutine(TimeCount());
	}

    int previousHours;
	private IEnumerator TimeCount()
	{
		while (true)
		{
			yield return Async.Wait(inGameTimeSpan.TotalSeconds);
			CurrentTime = new TimeSpan(CurrentTime.Hours, CurrentTime.Minutes + (int)minutesPerTimeSpan, CurrentTime.Seconds);

            if (previousHours != CurrentTime.Hours )
            {
                PlantSystem.Instance.ChangeResources(CurrentTime.Hours); //zmienia zużycie zasobów
            }
            previousHours = CurrentTime.Hours;

			if (CurrentTime.Days == 1)
			{
				StartNewDay();
				GameManager.Instance.HealthSys.Health.TakeValue(GameManager.Instance.HealthSys.Health.Max, "Death without Caution");
			}
		}
	}
}
