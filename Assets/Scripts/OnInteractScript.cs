using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

/// <summary>
/// OnInteract moet SendDataScript heten
/// Doel van OnInteractScript: Data van interactable ophalen en displayen
///     1. Object in MainTrigger wordt vastgelegd als dit een interactable is 
///     2. OnInteract function that performs
///     3. 
/// Kijken of dit later in een UIManager kan worden gerolled
/// </summary>

public class OnInteractScript : MonoBehaviour
{
    DataRecieverScript recieverScript;
    GameObject dataObject; // interactable
    [SerializeField]
    GameObject dataDisplayer; // InteractPanel

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            dataObject = other.gameObject;
        }
    }

    /// <summary>
    /// Functie die datareciever de RetrieveData functie uit laat voeren
    /// linked de interactable met de retrieveData functie in datareciever
    /// </summary>
    public void RequestRetrieveData()
    {
        //check if interactable has data atached to it
        if (dataObject.GetComponent<InteractableDataScript>() != null)
        {
            recieverScript = dataDisplayer.GetComponent<DataRecieverScript>();
            recieverScript.RetrieveData(dataObject);
        }
        else Debug.Log("no Data on Interactable");
    }


    public void ToggleInteractPanel()
    { 
        dataDisplayer.SetActive(true);
    }
}
