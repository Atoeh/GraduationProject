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
    [SerializeField]
    private GameObject lootUI;

    private bool movUIState;

    void Start()
    {
        movUIState = true;
    }

    void OnEnable()
    {
        //Events subscriben
        GameEvents.OnToggleMoveUI += ToggleMoveUI;
        GameEvents.OnChangeDarkness += ChangeDarkness;
        GameEvents.OnQuotaSet += SetQuotaUI;
        GameEvents.OnLootPickUp += UpdateLootUI;

        //Referenties aanmaken zoeken naar UI componenten
        //Dit later in plaats van alle serializefields met gameobjecten?
    }

    void OnDisable()
    {
        //Events unsubscriben
        GameEvents.OnToggleMoveUI -= ToggleMoveUI;
        GameEvents.OnChangeDarkness -= ChangeDarkness;
        GameEvents.OnLootPickUp -= UpdateLootUI;
        GameEvents.OnQuotaSet -= SetQuotaUI;
    }

    void ToggleMoveUI()
    {
        ToggleButtonsInChildren();
    }

    public void ChangeDarkness(float value)
    {
        DarkPanel.GetComponent<ChangeOpacityScript>().ChangeOpacity(value);
    }

    void SetQuotaUI(float value)
    { 
        //goal to set the quota that you are suposed to hit in the dungeon
        //Perhaps in start function of the game manager there is a SetQuotaEvent
        //GameManager listens to the Set
    }

    void UpdateLootUI(float value)
    {
        //Niet het doel dat dit de UpdateFunctie uitvoert maar de juiste gameobject 
        //De juiste code laat uitvoeren?
        //Is dit niet dubbel op met de gameEvent?
    }

    void ToggleButtonsInChildren()
    {
        //Deze code moet Het de MovementButtons cluster zefl uitvoeren.
        bool state =! movUIState;

        Button[] buttonArray = movementUI.GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttonArray)
        {
            btn.interactable = state;
        }
        movUIState = !movUIState;
    }

}