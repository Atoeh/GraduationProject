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

    [Header("- CutScenes and Panels -")]

    [SerializeField]
    private GameObject introPanel;
    [SerializeField]
    private GameObject outroPanel;
    [SerializeField]
    private GameObject[] quotaCutScenes;
    private int quotaSceneIndex;
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
            introPanel.GetComponent<CutSceneScripts>().transTime = transitionTime;
            IntroScreen();
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

        quotaSceneIndex = 0;
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
        Debug.Log("Setquota in UIManagaer");
        if (lootUI.GetComponent<LootUiScript>() != null)
        {
            if (lootUI.GetComponent<LootUiScript>().enabled)
                lootUI.GetComponent<LootUiScript>().SetQuotaUI(value);
        }
        else
            lootUI.GetComponent<LootUiScriptV2>().SetQuotaUI(value);

        if (quotaCutScenes != null)
            StartQuotaCutScene();
    }

    public void StartQuotaCutScene()
    {
        quotaCutScenes[quotaSceneIndex].GetComponent<CutSceneScripts>().StartCutScene();
        quotaSceneIndex++;
    }

    void UpdateLootUI(float value)
    {
        if (lootUI.GetComponent<LootUiScript>() != null)
        {
            if (lootUI.GetComponent<LootUiScript>().enabled)
                lootUI.GetComponent<LootUiScript>().UpdateUI(value);
        }
        else
            lootUI.GetComponent<LootUiScriptV2>().UpdateUI(value);
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
    void GoToMerchant()
    {
        outroPanel.GetComponent<CutSceneScripts>().StartCutScene();
    }

    void GameOverScreen()
    {
        gameOverPanel.GetComponent<CutSceneScripts>().StartCutScene();
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