using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cyberultimate.Unity;

public class CutsceneManager : MonoBehaviour
{
	[SerializeField]
	private CutsceneAsset[] cutsceneDialogue = null;


	[SerializeField]
	private AudioSource voiceSource = null;

	// [SerializeField]
	// private AudioClip voice = null;

	[SerializeField]
	private Text subtitles = null;

	private Coroutine displayingText = null;
	private Coroutine writeDisplayCoroutine = null;

	[SerializeField]
	private float textDisplayCooldown = 0.075f;

	private readonly Queue<CutsceneAsset> cutsceneText = new Queue<CutsceneAsset>();

	[SerializeField]
	private UnityEvent onCutsceneEnd = null;
	[SerializeField]
	private UnityEvent onCutsceneStart = null;
	
    protected void OnEnable()
	{
		SetupCutscene();
		onCutsceneStart.Invoke();
	}

	private void SetupCutscene()
	{
		foreach (var item in cutsceneDialogue)
		{
			cutsceneText.Enqueue(item);
		}

		displayingText = StartCoroutine(DisplayNextSentence());
	}

	private IEnumerator DisplayNextSentence()
	{
		while (true)
		{
			if (cutsceneText.Count == 0)
			{
				EndCutscene();
				yield break;
			}

			if (writeDisplayCoroutine != null)
			{
				StopCoroutine(writeDisplayCoroutine);
			}

			voiceSource.Stop();
			CutsceneAsset voiceSentence = cutsceneText.Dequeue();

			voiceSource.clip = voiceSentence?.VoiceLine;

			voiceSource.Play(); 
			subtitles.text = string.Empty;
			writeDisplayCoroutine = StartCoroutine(TypeTextSlowly(voiceSentence.Sentence, textDisplayCooldown, subtitles));

			yield return Async.WaitUnscaled(voiceSentence.CooldownToNext);
		}
	}

	public IEnumerator TypeTextSlowly(string text, float cooldown, Text displayText)
	{
		foreach (char c in text)
		{
			displayText.text += c;
			yield return Async.WaitUnscaled(cooldown);
		}
	}



	private void EndCutscene()
	{
		if (writeDisplayCoroutine != null)
		{
			StopCoroutine(writeDisplayCoroutine);
		}

		onCutsceneEnd.Invoke();

	}

	protected void OnDisable()
	{
		onCutsceneEnd.Invoke();
	}
}
