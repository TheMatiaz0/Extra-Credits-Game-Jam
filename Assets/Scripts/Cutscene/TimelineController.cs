using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Events;

public class TimelineController : MonoSingleton<TimelineController>
{
	private PlayableDirector director;

	[SerializeField]
	private GameObject pressToSkip = null;

	private Collider playerCollider = null;

	public bool CutsceneRunning { get; private set; } = true;

	[SerializeField]
	private UnityEvent onCutsceneEnd;

	[SerializeField]
	private UnityEvent onCutsceneStart;


	protected void Start()
	{
		playerCollider = MovementController.Instance.GetComponent<Collider>();

		director = GetComponent<PlayableDirector>();
	}

	public void LaunchCutscene()
	{
		playerCollider.enabled = false;
		MovementController.Instance.enabled = false;
		MouseLook.Instance.enabled = false;
		CanvasManager.Instance.ShowCutsceneCanvas();
		Music.Instance.gameObject.SetActive(false);
		AudioManager.Instance.gameObject.SetActive(false);
		onCutsceneStart.Invoke();
		CutsceneRunning = true;
	}

	public void CloseCutscene()
	{
		MovementController.Instance.enabled = true;
		MouseLook.Instance.enabled = true;
		CanvasManager.Instance.ShowMainCanvas();
		playerCollider.enabled = true;
		Music.Instance.gameObject.SetActive(true);
		AudioManager.Instance.gameObject.SetActive(true);
		onCutsceneEnd.Invoke();
		CutsceneRunning = false;
	}

	protected async void Update()
	{
		if (CutsceneRunning == false)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			director.time = director.playableAsset.duration;
			await Async.Wait(TimeSpan.FromSeconds(1));
			CloseCutscene();
		}

		if (Input.anyKeyDown == true)
		{
			pressToSkip.SetActive(true);
		}
	}
}
