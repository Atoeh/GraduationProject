using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainTriggerScript : MonoBehaviour
{
    /// <summary>
    /// MainTriggerScript should be named to DetectInteractableScript
    /// </summary>

    [SerializeField]
    private GameObject interact;
    private Button interactButton;
    //[SerializeField]
    //private GameObject fight;
    //private Button fightButton;

    private void Start()
    {
        interactButton = interact.GetComponent<Button>();
    }

    //toggles the interactButton if interactable is in trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            interactButton.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            interactButton.interactable = false;
        }
    }
}
