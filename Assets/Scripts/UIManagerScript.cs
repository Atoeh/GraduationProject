using JetBrains.Annotations;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    //Events --------------------
    public static event Action OnMovementToggle;

    //Objects -------------------
    [SerializeField]
    private GameObject interactUI;
    [SerializeField]
    private GameObject fightUI;

    [SerializeField]
    private GameObject movementUI;
    private bool movUIState;

    void Start()
    {
        CleanScreen();
        movUIState = true;
    }

    public void CleanScreen()
    { 
        interactUI.SetActive(false);
        fightUI.SetActive(false);
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
