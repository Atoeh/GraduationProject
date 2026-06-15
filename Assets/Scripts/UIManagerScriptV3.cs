using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScriptV3 : MonoBehaviour
{
    [Header("- UI Element Groups -")]
    
    [SerializeField]
    private GameObject movementUI;
    [SerializeField]
    private GameObject lootUI;
    [SerializeField]
    private GameObject candleTimerUI;
    [SerializeField]
    private GameObject darknessUi;

    [Header("- UI Panels -")]

    [SerializeField]
    private GameObject introPanel;
    [SerializeField]
    private GameObject outroPanel;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private float transitionTime;

    [Header("- Enabled Ui elements -")]

    [SerializeField]
    private bool enableLootUI = false;
    [SerializeField]
    private bool enableCandleTimer = false;
    private bool movUIState;

    void Start()
    {
        movUIState = true;

        if (introPanel != null)
        {
            IntroScreen();
            introPanel.GetComponent<CutSceneScripts>().transTime = transitionTime;
        }
        if (outroPanel != null)
            outroPanel.GetComponent<CutSceneScripts>().transTime = transitionTime;
        if (gameOverPanel != null)
            gameOverPanel.GetComponent<CutSceneScripts>().transTime = transitionTime;

        if (enableLootUI == false)
               lootUI.SetActive(false);

        if (enableCandleTimer == false)
        {
            darknessUi.SetActive(false);
            candleTimerUI.SetActive(false);
        }

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
        darknessUi.GetComponent<ChangeOpacityScript>().ChangeOpacity(value);
    }

    void SetQuotaUI(float value)
    {
        lootUI.GetComponent<LootUiScript>().SetQuotaUI(value);
    }

    void UpdateLootUI(float value)
    {
        lootUI.GetComponent<LootUiScript>().UpdateUI(value);
    }

    void ToggleButtonsInChildren()
    {
        //Deze code moet Het de MovementButtons cluster zefl uitvoeren.
        bool state = !movUIState;

        Button[] buttonArray = movementUI.GetComponentsInChildren<Button>(true);
        foreach (Button btn in buttonArray)
        {
            btn.interactable = state;
        }
        movUIState = !movUIState;
    }

    //Change this so that this executes the code within the merchant panel Thingy?
    void GoToMerchant(bool isQuotaHit)
    {
        //StartCoroutine(TransitionCoroutine());
        outroPanel.SetActive(true); // moet functie in script van Paneel zelf worden!!!!
        //StartCoroutine(TransitionCoroutine());
    }

    void GameOverScreen()
    {
        //StartCoroutine(TransitionCoroutine());
        gameOverPanel.SetActive(true); // moet functie in script van Paneel zelf worden!!!!
        //StartCoroutine(TransitionCoroutine());
        Debug.Log("Game Over Screen activated");
    }

    void IntroScreen()
    { 
        introPanel.GetComponent<CutSceneScripts>().StartCutScene();
    }

    void DisableScreen(GameObject Screen)
    {
        //StartCoroutine(TransitionCoroutine());
        gameObject.SetActive(false);
    }

    //public IEnumerator TransitionCoroutine()
    //{
    //    transitionUI.GetComponent<TransitionScript>().TransitionCalled(transitionTime);
    //    yield return new WaitForSeconds(transitionTime);
    //}
}