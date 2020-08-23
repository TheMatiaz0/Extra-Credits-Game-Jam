using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private bool once = true;
    [SerializeField] private float? duration = null;

    private bool used = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || CutsceneManager.Instance.CutscenePlaying) return;
        if (once && used) return;

        used = true;
        Debug.Log("Showing dialog text");
        UIManager.Instance.ShowDialogText(text, duration);
    }
}
