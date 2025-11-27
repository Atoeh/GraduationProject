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
/// </summary>

public class OnInteractScript : MonoBehaviour
{
    DataRecieverScript dataReciever;
    GameObject dataObject; // interactable
    [SerializeField]
    GameObject dataLocation; // InteractPanel

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            dataObject = other.gameObject;
        }
    }

    public void RequestRetrieveData()
    {
        //check if interactable has data atached to it
        if (dataObject.GetComponent<InteractableDataScript>() != null)
        {
            dataReciever = dataLocation.GetComponent<DataRecieverScript>();
            dataReciever.RetrieveData(dataObject);

            Debug.Log("OnInteractScript requested RetrieveData");
        }
        else Debug.Log("no Data on Interactable");
    }

    public void ToggleInteractPanel()
    { 
        dataLocation.SetActive(true);
    }

    public void ToggelFightPanel()
    { }
}
