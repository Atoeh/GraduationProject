using UnityEngine;
using UnityEngine.UIElements;
public class InteractableDataScript : MonoBehaviour
{
    public string recTitle;
    [TextAreaAttribute]
    public string recContent;
    public bool recButton1;
    public string recButton1Text;
    public bool recButton2;
    public string recButton2Text;

    /// <summary>
    /// Intentie, checken voor alle stats als iets niet is ingevuld dat het met een spatie vervangen kan worden bijv.
    /// </summary>

    private void Start()
    {
        if (recTitle == null)
        { 
            recTitle = gameObject.name;
        }
        if (recContent == null)
        { 
            recContent = string.Empty;
        }
    }
}
