using System;
using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    [SerializeField] private bool once = false;

    private bool used = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (GameEndingOptions.Instance.currentTimeline?.CutsceneRunning ?? false) return;
        if (!other.gameObject.CompareTag("Player")) return;
        if (once && used) return;

        Action();
        used = true;
    }

    protected abstract void Action();
}
