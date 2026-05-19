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
    private GameObject TavernPanel;
    [SerializeField]
    private GameObject MerchantPanel0;
    //[SerializeField]
    //private GameObject MerchantPanel1;
    [SerializeField]
    private GameObject CandleDiedPanel;
    [SerializeField]
    private GameObject lootUI;

    private bool movUIState;

    void Start()
    {
        movUIState = true;
        TavernPanel.SetActive(true);
        MerchantPanel0.SetActive(false);
        //MerchantPanel1?.SetActive(false);
        CandleDiedPanel.SetActive(false);
    }

    void OnEnable()
    {
        //Events subscriben
        GameEvents.OnToggleMoveUI += ToggleMoveUI;
        GameEvents.OnChangeDarkness += ChangeDarkness;
        GameEvents.OnQuotaSet += SetQuotaUI;
        GameEvents.OnInventoryUpdated += UpdateLootUI;
        GameEvents.OnOpenDoor += GoToMerchant;
        GameEvents.OnCandleDepleted += GameOverScreen;
    }

    void OnDisable()
    {
        //Events unsubscriben
        GameEvents.OnToggleMoveUI -= ToggleMoveUI;
        GameEvents.OnChangeDarkness -= ChangeDarkness;
        GameEvents.OnQuotaSet -= SetQuotaUI;
        GameEvents.OnInventoryUpdated -= UpdateLootUI;
        GameEvents.OnOpenDoor -= GoToMerchant;
        GameEvents.OnCandleDepleted -= GameOverScreen;
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
        lootUI.GetComponent<LootUiScript>().SetQuotaUI(value);
    }

    void UpdateLootUI(float value)
    {
        //Niet het doel dat dit de UpdateFunctie uitvoert maar de juiste gameobject 
        //De juiste code laat uitvoeren?
        //Is dit niet dubbel op met de gameEvent?
        lootUI.GetComponent<LootUiScript>().UpdateUI(value);
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

    void GoToLoop ()
    { 
    
    }

    //Change this so that this executes the code within the merchant panel Thingy?
    void GoToMerchant(bool isQuotaHit)
    {
        MerchantPanel0.SetActive(true);
        //if (isQuotaHit == true)
        //    MerchantPanel1.SetActive(true);
        //else MerchantPanel0.SetActive(true);
    }

    void GameOverScreen()
    {
        CandleDiedPanel.SetActive(true);
        Debug.Log("Game Over Screen activated");
    }
}