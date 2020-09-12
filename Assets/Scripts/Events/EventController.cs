using System;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventController : MonoSingleton<EventController>
{
    private ScriptableEvent[] allEvents;

    protected override void Awake()
    {
        base.Awake();
        allEvents = Resources.LoadAll<ScriptableEvent>("Events");
    }

    private void Start()
    {
        TimeManager.Instance.OnCurrentDayChange += OnDayChange;
    }

	protected void OnDisable()
	{
        TimeManager.Instance.OnCurrentDayChange -= OnDayChange;
    }

	private void OnDayChange(object sender, SimpleArgs<Cint> e)
    {
        if (e.Value == 0) return;
        if (Random.Range(0, 1) == 0) return;
        
        var evt = allEvents[Random.Range(0, allEvents.Length)];
        
        ActivateEvent(evt);
    }

    public void ActivateEvent(ScriptableEvent evt)
    {
        if(evt?.logic == null) return;

        Debug.Log($"Activating event: {evt.name}");
        
        var logic = Activator.CreateInstance(evt.logic) as EventLogic;

        logic.Activate();
    }
}