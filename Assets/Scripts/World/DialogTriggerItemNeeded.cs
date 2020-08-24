using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class DialogTriggerItemNeeded : MonoBehaviour
{
    [SerializeField] private string hasItemText;
    [SerializeField] private string doesntHaveItemtext;
    [SerializeField] private bool once = true;
    [SerializeField] private float duration = 3f;
    [SerializeField] private List<ItemScriptableObject> itemOptionsNeeded;

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (GameEndingOptions.Instance.currentTimeline?.CutsceneRunning ?? false) return;
        if (!other.gameObject.CompareTag("Player")) return;
        if (once && used) return;

        used = true;
        if (itemOptionsNeeded.Any(x=> Inventory.Instance.HasItem(x.name))) UIManager.Instance.ShowDialogText(hasItemText, duration); 
        else UIManager.Instance.ShowDialogText(doesntHaveItemtext, duration);
    }
}
