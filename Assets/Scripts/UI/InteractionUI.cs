using System;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InteractionUI : MonoSingleton<InteractionUI>
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private Text eKeyText;

        private CanvasGroup canvas;

        private void Start()
        {
            canvas = GetComponent<CanvasGroup>();
            HidePossibleInteraction();
        }

        public void ShowPossibleInteraction()
        {
            eKeyText.color = progressImage.color = Color.white;
            progressImage.fillAmount = 0;
        }

        public void SetPossibleInteractionProgress(float progress)
        {
            progressImage.fillAmount = progress;
        }

        public void HidePossibleInteraction()
        {
            eKeyText.color = progressImage.color = Color.clear;
            progressImage.fillAmount = 0;
        }

        public void Show()
        {
            canvas.alpha = 1;
        }

        public void Hide()
        {
            canvas.alpha = 0;
        }
    }
}