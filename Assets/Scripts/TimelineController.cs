using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{
	private PlayableDirector director;

	[SerializeField]
	private GameObject pressToSkip = null;


	protected void Start()
	{
		director = GetComponent<PlayableDirector>();
	}

	public void LaunchCutscene()
	{
		MovementController.Instance.enabled = false;
		MouseLook.Instance.enabled = false;
		CanvasManager.Instance.ShowCutsceneCanvas();
	}

	public void CloseCutscene()
	{
		MovementController.Instance.enabled = true;
		MouseLook.Instance.enabled = true;
		CanvasManager.Instance.ShowMainCanvas();
	}

	protected async void Update()
	{
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
