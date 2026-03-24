using UnityEngine;
using UnityEngine.UI;

public class ChangeOpacityScript : MonoBehaviour
{
    private Image image;
    [SerializeField]
    private float opacityStart = 0f;

    private void Start()
    {
        image = this.GetComponent<Image>();
        ChangeOpacity(opacityStart);
    }

    public void ChangeOpacity(float value)
    {
        float alpha = value *.9f;

        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}