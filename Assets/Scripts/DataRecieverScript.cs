using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Dataretriever script eist de data van interactables en enemeies op en heeft een functie om deze data te displayen
/// </summary>
public class DataRetrieverScript : MonoBehaviour
{
    //Containers for stats that are recieved

    string titleText;
    string contentText;
    bool button1Active;
    string button1Text;
    bool button2Active;
    string button2Text;
    private InteractableDataScript dataScript;

    //Containers for the ui elements that will be changed

    [SerializeField]
    private TextMeshPro titleContainer;
    [SerializeField]
    private TextMeshPro contentContainer; 
    [SerializeField]
    GameObject button1;
    [SerializeField]
    GameObject button2;

    //order of things that happen on button click OnInteractScript will send the gameobject reference if it contains
    public void RetrieveData(GameObject dataObject)
    {
        dataScript = dataObject.GetComponent<InteractableDataScript>();
        titleText = dataScript.recTitle;
        contentText = dataScript.recContent;
        button1Active = dataScript.recButton1;
        button2Active= dataScript.recButton2;
    }
    public void WriteInteractPanel()
    { 
        titleContainer.text = titleText;
        contentContainer.text = contentText;
        button1.GetComponent<Button>().text = button1Text;

        if (button2Active == true)
        {
            button2.SetActive(true);
            button2.GetComponent<Button>().text = button1Text;
        }
    }

    public void ReadInteractPanel() 
    { 
    
    }
}
