using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

/// <summary>
/// DataRetriever script eist de data van interactables en enemeies op en heeft een functie om deze data te displayen
/// </summary>
public class DataRecieverScript : MonoBehaviour
{
    //container Names of recieved info
    string titleText;
    string contentText;
    private InteractableDataScript dataScript;

    //Containers for the ui elements that will be changed
    [SerializeField]
    private GameObject titleContainer;
    private TMP_Text title;
    [SerializeField]
    private GameObject contentContainer;
    private TMP_Text content; 

    private void Start()
    {
        title = titleContainer.GetComponent<TMP_Text>();
        content = contentContainer.GetComponent<TMP_Text>();
    }

    //order of things that happen on button click OnInteractScript will send the gameobject reference if it contains
    public void RetrieveData(GameObject dataObject)
    {
        dataScript = dataObject.GetComponent<InteractableDataScript>();
        titleText = dataScript.recTitle;
        contentText = dataScript.recContent;
    }

    public void WriteInteractPanel()
    {
        if (titleText != null && contentText != null)
        {
            title.text = titleText;
            content.text = contentText;
        }
    }
}
