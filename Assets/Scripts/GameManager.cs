using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("- Quota variables -")]

    [SerializeField] private bool quotaIsAll = false;

    private bool quotaPrevReached;

    [Header("- Quota Array variables -")]

    [SerializeField] private int quotaIndex;
    [SerializeField] private float[] levelQuotas;

    [Header("- Scene variables -")]

    //[SerializeField] private Scene start;
    private int startScene = 0;
    private int currentScene;

    [Header("- Game States -")]
    [SerializeField] private bool[] gameState;


    // --------------------- SETUP ---------------------

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnTimerPause += PauseGame;
        GameEvents.OnTimerRanOut += TriggerGameOver;
        GameEvents.OnLeaveGame += RestartGame;
    }

    private void OnDisable()
    {
        GameEvents.OnTimerPause += PauseGame;
        GameEvents.OnTimerRanOut -= TriggerGameOver;
        GameEvents.OnLeaveGame -= RestartGame;
    }

    // --------------------- LOADING LEVELS ---------------------

    public void RestartLevel()
    {
        Debug.Log("Scene management: Reload current scene");
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Scene management: Load next level");
        //next scene met modulo (%) dat het terug warped naar de eerste scene uit de index...
        currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = (currentScene + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextScene);
    }

    public void RestartGame()
    {
        Debug.Log("Scene management: Load Main Menu");
        SceneManager.LoadScene(startScene);
    }

    // --------------------- GAMESTATE CHANGES ---------------------
    public void CompareQuota(float value)
    {
        quotaIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(quotaIndex + " = quotaIndex");

        if (value >= levelQuotas[quotaIndex] && !quotaPrevReached)
        {
            quotaPrevReached = true;
            GameEvents.QuotaHit();
        }
        else if (value <= levelQuotas[quotaIndex] && quotaPrevReached)
        {
            quotaPrevReached = false;
            GameEvents.QuotaLost();
        }

        Debug.Log("collected vs quota: " + value + " / " + levelQuotas[quotaIndex]);
    }

    public void TriggerGameOver()
    {
        Debug.Log("Gamestate: Open Gameover menu");
        TimerManager.Instance.PauseTimer();

        //Timerinstance pause
        //UI instance open GameOver panel
    }

    public void PauseGame()
    {
        Debug.Log("Gamestate: Open Pause menu");
        TimerManager.Instance.PauseTimer();

        //Timerinstance pause
        //Open pause UI element Ui instance
    }

    public void ResumeGame()
    {
        Debug.Log("Gamestate: Resume game");
        TimerManager.Instance.ResumeTimer();

        //Close pause UI element
        //Timerinstance pause
    }

    public void StartDungeoneering()
    {
        Debug.Log("GameState: close intro and start with dungeoneering");
        if (TimerManager.Instance)
        {
            TimerManager.Instance.ResetTimer();
            TimerManager.Instance.ResumeTimer();
        }
        else
            Debug.Log("StartDungeoneering Not performed : missing TimerManager");
    }

    public void StartInteraction(GameObject cutScene)
    {
        Debug.Log("Gamestate: Start NPC interaction or other custscene");
        TimerManager.Instance.PauseTimer();

        //Timer instance put on pause
        //Ui instance open interact Cutscene
        //Starts interaction with NPC's ???
    }

    public void StopInteraction()
    {
        Debug.Log("Gamestate: Start NPC interaction or other custscene");
        TimerManager.Instance.ResumeTimer();

        //Timer instance resume
        //Ui instance open interact Cutscene
    }
}
