using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MovementTriggerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject movementButton;
    private Button button;
    private bool movementEnabled;
    private bool isLocked;


    private void Start()
    {
        movementEnabled = true;
        button = movementButton.GetComponent<Button>();
        isLocked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Edge")
        {
            button.interactable = false;
            movementEnabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Edge")
        {
            button.interactable = true;
            movementEnabled = true;
        }
    }

    public void MovementLockToggle()
    {
        bool isLockedOld = isLocked;

        //Change interactability based on isLocked
        if (isLockedOld == false)
        {
            button.interactable = false;
        } else
            if (movementEnabled == true)
                button.interactable = true;
    
        isLocked = !isLockedOld;
    }
}
