using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        public CanvasGroup canvas;
        private void Start()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                LeanTween.alphaCanvas(canvas, 0, 1f).setEaseInOutCubic().setOnComplete(StartGame);
            }
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}