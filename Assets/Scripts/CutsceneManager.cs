using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
	[SerializeField]
	private CutsceneAsset[] cutsceneDialogue = null;


	[SerializeField]
	private AudioSource voiceSource = null;

	[SerializeField]
	private AudioClip voice = null;

	[SerializeField]
	private Text subtitles = null;

	[SerializeField]
	private UnityAction actionAfterCutsceneEnd = delegate { };

	private Coroutine displayingText = null;
	private Coroutine writeDisplayCoroutine = null;

	[SerializeField]
	private float textDisplayCooldown = 0.075f;

	[SerializeField]
	private GameObject pressToSkip = null;

	private readonly Queue<CutsceneAsset> cutsceneText = new Queue<CutsceneAsset>();

	[SerializeField]
	private UnityEvent onCutsceneEnd = null;


	protected void OnEnable()
	{
		SetupCutscene();
	}

	private void SetupCutscene()
	{
		foreach (var item in cutsceneDialogue)
		{
			cutsceneText.Enqueue(item);
		}

		displayingText = StartCoroutine(DisplayNextSentence());
	}

	protected void Update()
	{
		if (pressToSkip != null)
		{
			if (Input.anyKeyDown == true)
			{
				pressToSkip.SetActive(true);
			}
		}

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

			CutsceneAsset spriteWithSentence = cutsceneText.Dequeue();
			subtitles.text = string.Empty;
			writeDisplayCoroutine = StartCoroutine(TypeTextSlowly(spriteWithSentence.Sentence, textDisplayCooldown, subtitles));

			yield return Async.WaitUnscaled(spriteWithSentence.CooldownToNext);
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

		actionAfterCutsceneEnd.Invoke();
	}
}
