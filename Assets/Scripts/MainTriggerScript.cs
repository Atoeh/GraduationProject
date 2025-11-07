using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainTriggerScript : MonoBehaviour
{
    /// <summary>
    /// MainTriggerScript should be named to DetectionScript
    /// </summary>

    [SerializeField]
    private GameObject interactButton;
    private Button interact;
    [SerializeField]
    private GameObject fightButton;
    private Button fight;
    private GameObject interactableObject;

    [SerializeField]
    private GameObject interactScreen;

    [SerializeField]
    private GameObject fightScreen;

    private void Start()
    {
        interact = interactButton.GetComponent<Button>();
        fight = fightButton.GetComponent<Button>();
    }


    private InteractableDataScript dataScript;

    public void OnInteract(GameObject gameObject)
    {
        if (gameObject.GetComponent<InteractableDataScript>() != null)
        {
            dataScript = gameObject.GetComponent<InteractableDataScript>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
        {
            interact.interactable = true;
        }

        if (other.tag == "Enemy")
        {
            fight.interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            interact.interactable = false;
        }

        if (other.tag == "Enemy")
        {
            fight.interactable = false;
        }
    }
}
