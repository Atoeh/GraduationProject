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
    [SerializeField]
    private GameObject DarkPanel;

    private bool movUIState;

    void Start()
    {
        movUIState = true;
    }

    void OnEnable()
    {
        GameEvents.OnToggleMoveUI += ToggleMoveUI;
        GameEvents.OnChangeDarkness += ChangeDarkness;
    }

    void OnDisable()
    {
        GameEvents.OnToggleMoveUI -= ToggleMoveUI;
        GameEvents.OnChangeDarkness -= ChangeDarkness;
    }

    void ToggleMoveUI()
    {
        ToggleButtonsInChildren();
    }

    public void ChangeDarkness(float value)
    {
        DarkPanel.GetComponent<ChangeOpacityScript>().ChangeOpacity(value);
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