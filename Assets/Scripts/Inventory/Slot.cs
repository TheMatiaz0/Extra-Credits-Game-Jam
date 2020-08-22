using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image bgImage;

    public bool Selected
    {
        get => bgImage.color.a > 0.5f;
        set => bgImage.color = new Color(255, 255, 255, value ? 0.6f : 0.2f);
    }
    public Image image;
    
}
