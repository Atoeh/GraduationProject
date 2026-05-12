using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class OnInteractScript : MonoBehaviour
{
    GameObject dataObject;
    [SerializeField]

    private GameObject uiPrefab;
    private GameObject uiInstance;
    public Transform canvas;

    private bool isInstantiated = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactable")
        {
            dataObject = other.gameObject;
        }
    }

    public void InstantiateInteractPanel()
    {
        if (!isInstantiated)
        {
            if (dataObject.GetComponent<InteractableDataScript>() != null)
            {
                uiPrefab = dataObject.GetComponent<InteractableDataScript>().uiPrefab;
                uiInstance = Instantiate(uiPrefab, canvas);
                //send reference of the uiSpawner to the popup
                uiInstance.GetComponent<DestroyUiSpawnerScript>().Setup(dataObject);
                Debug.Log("Instatiate UIpopup");
            }
            else Debug.Log("no Data on Interactable");
            isInstantiated = true;
        }
        else
        {
            DeInstantiate();
        }
    }

    public void DeInstantiate()
    {
        Debug.Log("Deinstantiate");
        isInstantiated = false;
        Destroy(uiInstance);
    }
}
