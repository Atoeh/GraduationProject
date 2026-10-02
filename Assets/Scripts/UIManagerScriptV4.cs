using UnityEngine;
using UnityEngine.UI;

public class UIManagerScriptV4 : MonoBehaviour
{
    [Header("- UI Element Groups -")]

    [SerializeField] private GameObject candleTimerUI;
    [SerializeField] private GameObject darknessUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject movementUI;

    [Header("- CutScenes and Panels -")]

    [SerializeField] private GameObject introObject;
    [SerializeField] private GameObject outroObject;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float transitionTime;

    private CutSceneScripts introCutScene;
    private CutSceneScripts outroCutScene;
    private CutSceneScripts gameOverCutScene;

    [Header("- Enabled Ui elements -")]

    [SerializeField] private bool enableCandleTimer = false;
    private bool movUIState;

    void OnEnable()
    { 
        //Events subscriben

        //GameEvents.OnStartLevel
        GameEvents.OnExitLevel += StartOutro;
        GameEvents.OnTimerRanOut += StartGameOver;

        GameEvents.OnToggleMoveUI += ToggleMoveUI;
        //GameEvents.OnChangeDarkness += ChangeDarkness;
    }

    void OnDisable()
    {
        //Events unsubscriben

        GameEvents.OnExitLevel -= StartOutro;
        GameEvents.OnTimerRanOut -= StartGameOver;

        GameEvents.OnToggleMoveUI -= ToggleMoveUI;
        //GameEvents.OnChangeDarkness -= ChangeDarkness;
    }

    void Start()
    {
        //is it smart to set the transition time through here?
        if (introObject != null)
        { 
            introCutScene = introObject.GetComponent<CutSceneScripts>(); 
            introCutScene.transTime = transitionTime;
        }

        if (outroObject != null)
        {
            outroCutScene = outroObject.GetComponent<CutSceneScripts>();
            outroCutScene.transTime = transitionTime;
        }

        if (gameOverPanel != null)
        {
            gameOverCutScene = gameOverPanel.GetComponent<CutSceneScripts>();
            gameOverCutScene.transTime = transitionTime;
        }

        movUIState = true;

        if (enableCandleTimer == false)
        {
            darknessUI.SetActive(false);
            candleTimerUI.SetActive(false);
        }

        StartIntro();
    }

    void ToggleMoveUI()
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

    void StartIntro()
    {
        introCutScene.StartCutScene();
    }

    void StartOutro()
    {
        outroCutScene.StartCutScene();
    }

    void StartGameOver() 
    { 
        gameOverCutScene.StartCutScene(); 
    }
}