using UnityEngine;
using UnityEngine.UI;

public class UIManagerScriptV4 : MonoBehaviour
{
    public static UIManagerScriptV4 Instance;

    [Header("- UI Element Groups -")]

    [SerializeField] private GameObject candleTimerUI;
    [SerializeField] private GameObject darknessUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject movementUI;

    [Header("- CutScenes and Panels -")]

    [SerializeField] private GameObject introObject;
    [SerializeField] private GameObject outroObject;
    [SerializeField] private GameObject gameOverPanel;

    private CutSceneScripts introCutScene;
    private CutSceneScripts outroCutScene;
    private CutSceneScripts gameOverCutScene;

    private GameObject cutSceneInstance;
    private CutSceneScripts cutSceneScript;

    [SerializeField] private float transitionTime;

    [Header("- Enabled Ui elements -")]

    [SerializeField] private bool enableCandleTimer = false;
    private bool movUIState;

    private void Awake()
    {
        Instance = this;
    }

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
        //if (introObject != null)
        //{ 
        //    introCutScene = introObject.GetComponent<CutSceneScripts>(); 
        //    introCutScene.transTime = transitionTime;
        //}

        //if (outroObject != null)
        //{
        //    outroCutScene = outroObject.GetComponent<CutSceneScripts>();
        //    outroCutScene.transTime = transitionTime;
        //}

        //if (gameOverPanel != null)
        //{
        //    gameOverCutScene = gameOverPanel.GetComponent<CutSceneScripts>();
        //    gameOverCutScene.transTime = transitionTime;
        //}

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
        StartCutScene(introObject, null);
    }

    void StartOutro()
    {
        outroCutScene.StartCutScene();
    }

    void StartGameOver() 
    { 
        gameOverCutScene.StartCutScene(); 
    }

    public void StartCutScene(GameObject cutScene, LootPickUpV3 spawnItem)
    {
        //setup of cutsccene
        cutSceneInstance = Instantiate(cutScene);
        cutSceneScript = cutSceneInstance.GetComponent<CutSceneScripts>();
        
        cutSceneScript.transTime = transitionTime;
        if (spawnItem != null)
            cutSceneScript.spawner = spawnItem;
        
        cutSceneScript.StartCutScene();
    }

}