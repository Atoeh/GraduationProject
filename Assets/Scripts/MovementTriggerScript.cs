using UnityEngine;
using UnityEngine.UI;

public class MovementTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject movementButton;
    private Button button;
    private bool movementEnabled;

    private void Start()
    {
        button = movementButton.GetComponent<Button>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Edge")
        {
            button.interactable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Edge")
        { 
            button.interactable = true;
        }
    }
}
