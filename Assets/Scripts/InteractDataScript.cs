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
        if (recButton1Text == null)
        {
            recButton1 = true;
            recButton1Text = "Continue";
            recButton2 = false;
        }
        if (recButton2 == false)
            recButton2Text = string.Empty;
    }
}
