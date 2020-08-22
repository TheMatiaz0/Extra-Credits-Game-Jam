using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class InteractionGridSlot : MonoBehaviour
    {
        public string interactionName;
        public Image image;
        public UnityEvent onClick;
        
        public void PointerEnter()
        {
            LeanTween.scale((RectTransform) transform, Vector3.one * 1.1f, 0.2f).setEaseInOutBounce();
            InteractionGrid.Instance.ShowTooltip(interactionName);
        }

        public void PointerExit()
        {
            LeanTween.scale((RectTransform) transform, Vector3.one, 0.2f).setEaseInOutBounce();
            InteractionGrid.Instance.HideTooltip();
        }

        public void PointerDown()
        {
            onClick?.Invoke();
            InteractionGrid.Instance.Hide();
        }
    }
}