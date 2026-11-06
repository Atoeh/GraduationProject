using UnityEngine;
using UnityEngine.UI;

public class UIManagerScriptV4 : MonoBehaviour
{
    public static UIManagerScriptV4 Instance;

    [Header("- UI Element Groups -")]

    // [SerializeField] private GameObject candleTimerUI;
    [SerializeField] private GameObject darknessUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject movementUI;

    [Header("- CutScenes and Panels -")]

    [SerializeField] private GameObject introObject;
    [SerializeField] private GameObject outroObject;
    [SerializeField] private GameObject gameOverObject;

    private CutSceneScripts introCutScene;
    private CutSceneScripts outroCutScene;
    private CutSceneScripts gameOverCutScene;

    private GameObject cutSceneInstance;
    private CutSceneScripts cutSceneScript;

    [SerializeField] private float transitionTime;

    [Header("- Enabled Ui elements -")]

    //[SerializeField] private bool enableCandleTimer = false;
    private bool movUIState;

    private void Awake()
    {
        Instance = this;
        // candleTimerUI = TimerManager.Instance.gameObject;
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
        movUIState = true;
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
        //outroCutScene.StartCutScene();
        StartCutScene(outroObject, null);
    }

    void StartGameOver()
    {
        StartCutScene(gameOverObject, null);
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