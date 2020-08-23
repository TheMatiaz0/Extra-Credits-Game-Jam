﻿using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private string text;
    [SerializeField] private bool once = true;
    [SerializeField] private float duration = 0;

    private bool used = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (once && used) return;
        used = true;
        Debug.Log("Showing dialog text");
        UIManager.Instance.ShowDialogText(text,(duration==0)?0:duration);
    }
}
