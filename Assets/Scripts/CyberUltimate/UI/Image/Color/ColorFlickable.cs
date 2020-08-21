using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Cyberultimate.Unity
{
#pragma warning disable IDE0044
    public class ColorFlickable : MonoBehaviour
    {
        [SerializeField] private Color32[] colors = null;


        public Graphic Graphics { get; private set; }

        [SerializeField] [Range(0f, 5f)] private float delay = 3;
        protected void Awake()
        {
            Graphics = this.gameObject.GetComponent<Graphic>();
        }
        protected void Start()
        {
            StartCoroutine(Flash());
        }
        private void SwitchColors(Color32 color)
        {
            Graphics.color = color;

        }
        private IEnumerator Flash()
        {
            while (true)
            {
                foreach (Color32 item in colors)
                {
                    SwitchColors(item);
                    yield return Async.Wait(delay);
                }
            }
        }
    }
}