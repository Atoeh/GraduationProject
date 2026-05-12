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
    [SerializeField]
    private GameObject fight;
    private Button fightButton;

    private void Start()
    {
        interactButton = interact.GetComponent<Button>();
        fightButton = fight.GetComponent<Button>();
    }

    /// <summary>
    /// Detection that toggles the interactability of the interactButton and fightbutton
    /// Deze moet later een functie in de UIManager laten executeren ipv dit zelf te doen...
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable")
            Enter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
            Exit();
    }

    public void Enter()
    {
        interactButton.interactable = true;
        //if (coll.tag == "Enemy")
        //    fightButton.interactable = true;
    }

    public void Exit()
    { 
        //if (coll.tag == "Interactable")
        interactButton.interactable = false;
        //if (coll.tag == "Enemy")
        //    fightButton.interactable=false;

    }
}
