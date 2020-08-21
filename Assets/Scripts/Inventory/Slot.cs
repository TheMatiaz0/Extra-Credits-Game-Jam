using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image bgImage;

    public bool Selected
    {
        get => bgImage.color.a > 0.5f;
        set => bgImage.color = new Color(255, 255, 255, value ? 1 : 0.5f);
    }
    public Image image;
    
}
