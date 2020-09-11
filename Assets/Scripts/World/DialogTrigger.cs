using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] [TextArea] private string text;
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
        if (voiceline != null)
        {
            UIManager.Instance.ShowDialogText(text, voiceline);
        }
        else
        {
            UIManager.Instance.ShowDialogText(text, duration);
        }
    }
}
