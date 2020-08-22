using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class InteractionWheelSlot : MonoBehaviour
    {
        public Image image;
        public UnityEvent onClick;
        
        public void PointerEnter()
        {
            LeanTween.scale((RectTransform) transform, Vector3.one * 1.1f, 0.2f).setEaseInOutBounce();
        }

        public void PointerExit()
        {
            LeanTween.scale((RectTransform) transform, Vector3.one, 0.2f).setEaseInOutBounce();
        }

        public void PointerDown()
        {
            onClick?.Invoke();
            InteractionGrid.Instance.Hide();
        }
    }
}