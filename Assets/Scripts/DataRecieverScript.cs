using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DataRecieverScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro titleContainer;
    [SerializeField]
    private TextMeshPro contentContainer;
    string titleText;
    string contentText;
    bool button1;
    string button1Text;
    bool button2;
    string button2Text;
    private InteractableDataScript dataScript;

    public void RevieveData(GameObject dataObject)
    {
        dataScript = dataObject.GetComponent<InteractableDataScript>();
        titleText = dataScript.recTitle;
        contentText = dataScript.recContent;
        button1 = dataScript.recButton1;
        button2 = dataScript.recButton2;
    }
}
