using UnityEngine;
using UnityEngine.UIElements;
public class InteractableDataScript : MonoBehaviour
{
    public string title;
    [TextAreaAttribute]
    public string content;
    public bool button1;
    public string button1Text;
    public bool button2;
    public string button2Text;

    /// <summary>
    /// Intentie, checken voor alle stats als iets niet is ingevuld dat het met een spatie vervangen kan worden bijv.
    /// </summary>

    private void Start()
    {
        if (gameObject.tag != "Interactable")
            gameObject.tag = "Interactable";

        if (title == null)
        { 
            title = gameObject.name;
        }
        if (content == null)
        { 
            content = string.Empty;
        }
    }
}
