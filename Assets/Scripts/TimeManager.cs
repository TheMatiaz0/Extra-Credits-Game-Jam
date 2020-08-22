using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    private const float RealSecondsPerIngameDay = 120;
    private const byte HourPerDay = 24;
    private const float MinutesPerHour = 60;
	private Stopwatch t;

	protected void Start()
	{
		t = new Stopwatch();
		t.Start();
	}
}
