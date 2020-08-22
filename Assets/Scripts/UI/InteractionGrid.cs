using System;
using System.Collections.Generic;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class InteractionGrid : MonoSingleton<InteractionGrid>
    {
        [Serializable]
        public class Interaction
        {
            public string interactionName;
            public Sprite sprite;
            public UnityEvent action;
        }

        public Text tooltipText;
        public CanvasGroup interactionWheel;
        public RectTransform slots;
        public GameObject interactionPrefab;

        public float appearanceTime = 0.2f;
        
        protected override void Awake()
        {
            base.Awake();
            interactionWheel.alpha = 0;
            HideTooltip();
        }

        public void Show(IList<Interaction> interactions)
        {
            slots.KillAllChildren();
            Display(interactions);
            LeanTween.alphaCanvas(interactionWheel, 1f, appearanceTime).setEaseInOutCubic();
            MovementController.Instance.blockAiming = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Hide()
        {
            HideTooltip();
            LeanTween.alphaCanvas(interactionWheel, 0f, appearanceTime).setEaseInOutCubic();
            MovementController.Instance.blockAiming = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Display(IList<Interaction> interactions)
        {
            for(var i=0; i<9; i++)
            {
                var interaction = i < interactions?.Count ? interactions[i] : null;
                
                var g = Instantiate(interactionPrefab, slots);
                
                var slot = g.GetComponent<InteractionGridSlot>();
                slot.interactionName = interaction?.interactionName;
                slot.image.sprite = interaction?.sprite;
                slot.onClick = interaction?.action;
            }
        }

        public void ShowTooltip(string text)
        {
            tooltipText.text = text;
            tooltipText.gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            tooltipText.gameObject.SetActive(false);
        }
    }
}