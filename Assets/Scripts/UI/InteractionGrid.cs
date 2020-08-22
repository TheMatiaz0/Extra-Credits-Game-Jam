using System;
using System.Collections.Generic;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class InteractionGrid : MonoSingleton<InteractionGrid>
    {
        [Serializable]
        public class Interaction
        {
            public Sprite sprite;
            public UnityEvent action;
        }
        
        public CanvasGroup interactionWheel;
        public RectTransform slots;
        public GameObject interactionPrefab;

        public float appearanceTime = 0.2f;
        
        protected override void Awake()
        {
            base.Awake();
            interactionWheel.alpha = 0;
        }

        public void Show(IReadOnlyList<Interaction> interactions)
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
            LeanTween.alphaCanvas(interactionWheel, 0f, appearanceTime).setEaseInOutCubic();
            MovementController.Instance.blockAiming = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Display(IReadOnlyList<Interaction> interactions)
        {
            for(var i=0; i<9; i++)
            {
                var interaction = i < interactions?.Count ? interactions[i] : null;
                
                var g = Instantiate(interactionPrefab, slots);
                
                var slot = g.GetComponent<InteractionWheelSlot>();
                slot.image.sprite = interaction?.sprite;
                slot.onClick = interaction?.action;
            }
        }
    }
}