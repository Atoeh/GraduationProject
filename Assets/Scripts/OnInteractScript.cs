using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

/// <summary>
/// Goal of OnInteractScript is twofold: 1 create link between InteractableObject detected in MainTrigger and OnInteractFunction, 2 OnInteract function that performs
/// </summary>
public class OnInteractScript : MonoBehaviour
{
    DataRetrieverScript dataReciever;
    GameObject dataObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            dataObject = other.gameObject;
        }
    }

    public void OnInteract(GameObject gameObject)
    {
        if (gameObject.GetComponent<InteractableDataScript>() != null)
        {
            dataReciever = gameObject.GetComponent<DataRetrieverScript>();
            dataReciever.RetrieveData(dataObject);
        }
    }
}
