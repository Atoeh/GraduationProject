using JetBrains.Annotations;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    //Objects -------------------
    [SerializeField]
    private GameObject movementUI;
    private bool movUIState;

    void Start()
    {
        movUIState = true;
    }

    void OnEnable()
    {
        GameEvents.OnToggleMoveUI += ToggleMoveUI;
    }

    void OnDisable()
    {
        GameEvents.OnToggleMoveUI -= ToggleMoveUI;
    }

    void ToggleMoveUI()
    {
        ToggleButtonsInChildren();
    }
    
    void ToggleButtonsInChildren()
    {
        bool state =! movUIState;

        Button[] buttonArray = movementUI.GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttonArray)
        {
            btn.interactable = state;
        }
        movUIState = !movUIState;
    }

}
