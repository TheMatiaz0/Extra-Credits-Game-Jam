using System;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlantNeedsUI : MonoSingleton<PlantNeedsUI>
    {
        private CanvasGroup canvas;
        private bool Visible => canvas.alpha > 0;

        [SerializeField] private Image sunlight, soil, water;

        protected override void Awake()
        {
            base.Awake();
            canvas = GetComponent<CanvasGroup>();
            canvas.alpha = 0;
        }

        public void Show()
        {
            LeanTween.alphaCanvas(canvas, 1f, 0.2f);

            var p = PlantSystem.Instance;
            sunlight.fillAmount = p.Sunlight.Value / 100;
            soil.fillAmount = p.Soil.Value / 100;
            water.fillAmount = p.Soil.Value / 100;

            InteractionChecker.Instance.checkInteractions = false;
            MouseLook.Instance.BlockAiming = true;
        }

        public void Hide()
        {
            LeanTween.alphaCanvas(canvas, 0, 0.2f);
            InteractionChecker.Instance.checkInteractions = true;
            MouseLook.Instance.BlockAiming = false;
        }
        
        private void Update()
        {
            if (Visible && Input.GetKeyDown(KeyCode.E))
            {
                Hide();
            }
        }
    }
}