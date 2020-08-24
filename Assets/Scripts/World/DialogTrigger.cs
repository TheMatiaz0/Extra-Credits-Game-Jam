using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private AudioClip voiceline;
    [SerializeField] private bool once = true;
    [SerializeField] private float duration = 3f;

    private bool used = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (GameEndingOptions.Instance.currentTimeline?.CutsceneRunning ?? false) return;
        if (!other.gameObject.CompareTag("Player")) return;
        if (once && used) return;

        used = true;
        Debug.Log("Showing dialog text");
        UIManager.Instance.ShowDialogText(text, duration);
        AudioManager.Instance.PlayClip(voiceline);
    }
}
