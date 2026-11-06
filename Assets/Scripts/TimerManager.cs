using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("- Timer Setttings -")]
    [SerializeField] private float timeLeft = 0f;
    [SerializeField] private float updateTime = 2f;

    private bool candleIsPaused = true;
    private bool candleIsOn = false;
    private float timeSinceLastStep = 0f;
    public float timeMax;
    private bool timerRestarted = false;

    [Header("- Timer Array -")]
    [SerializeField] private float[] timerArray;
    public int timerIndex;

    [Header("- Timer UI Elements -")]
    [SerializeField] private GameObject totalUI;
    [SerializeField] private GameObject textObject;
    private TMP_Text timerText;
    [SerializeField] private Slider slider;

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

        timerIndex = SceneManager.GetActiveScene().buildIndex;
        timerText = textObject.GetComponent<TMP_Text>();
        timeMax = timerArray[timerIndex];
    }

    private void OnEnable()
    {
        GameEvents.OnTimerRanOut += PauseTimer;
        GameEvents.OnExitLevel += PauseTimer;
        GameEvents.OnExitLevel += NextTimer;
    }

    private void OnDisable()
    {
        GameEvents.OnTimerRanOut -= PauseTimer;
        GameEvents.OnExitLevel -= PauseTimer;
        GameEvents.OnExitLevel -= NextTimer;
    }

    void Update()
    {
        if (!candleIsPaused && candleIsOn)
        {
            //Is there still time left?
            if (timeLeft <= 0f && !timerRestarted)
            {
                timerRestarted = true;
                Debug.Log("Timer ran out");
                GameEvents.TimerRanOut();
            }

            //if timeSincelastStep is larger then a second, subtract second from timer.
            timeSinceLastStep += Time.deltaTime;
            if (timeSinceLastStep >= updateTime)
            {
                timeLeft -= updateTime;
                //Debug.Log("TimeLeft: " + timeLeft);
                SetSlider();
                SetText();
                timeSinceLastStep = 0;
            }
        }
    }

    // ----------------------- STANDARD TIMER -------------------------
    public void AddTime(float timeAdded)
    {
        if ((timeLeft + timeAdded) < timeMax)
        {
            timeLeft += timeAdded;
        }
        else
        {
            timeLeft = timeMax;
        }
        SetSlider();
        SetText();
        timerRestarted = false;
    }

    public void PauseTimer()
    {
        candleIsPaused = true;
        Debug.Log("Timer paused");
    }

    public void ResumeTimer()
    {
        candleIsPaused = false;
        timeSinceLastStep = 0;
        Debug.Log("Timer Resumed");
    }

    // ----------------------- SWITCH TIMER ON NEW LEVEL -------------------------
    public void NextTimer()
    {
        Debug.Log("NextTimer called");
        timerIndex = (timerIndex + 1) % timerArray.Length;
    }

    public void ResetTimer()
    {
        timeLeft = 0f;
        timerIndex = SceneManager.GetActiveScene().buildIndex;
        timeMax = timerArray[timerIndex];

        if (timeMax > 0)
        {
            ActivateTimer();
            AddTime(timeMax);
        }

        if (timeMax <= 0 || SceneManager.GetActiveScene().buildIndex == 0)
        {
            DeactivateTimer();
        }

        Debug.Log("TimerReset, current timeMax = " + timeMax);
    }

    // -------------------- TIMER VISUAL -------------------------
    private void SetSlider()
    {
        slider.value = timeLeft / timeMax;
    }

    private void SetText()
    {
        timerText.text = timeLeft.ToString();
    }

    private void ActivateTimer()
    {
        candleIsOn = true;
        totalUI.SetActive(true);
        Debug.Log("Candle Turned on");
    }

    private void DeactivateTimer()
    {
        candleIsOn = false;
        totalUI.SetActive(false);
        Debug.Log("Candle Turned off");
    }
}