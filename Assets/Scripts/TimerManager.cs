using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("- Timer Setttings -")]
    [SerializeField] private float timeLeft = 0f;
    [SerializeField] private float updateTime = 2f;

    private bool candleOn = false;
    private float timeSinceLastStep = 0f;
    private float timeMax;
    private bool timerRestarted = false;

    [Header("- Timer Array -")]
    [SerializeField] private float[] timerArray;
    [SerializeField] private int timerIndex = 0;

    [Header("- Timer UI Elements -")]
    [SerializeField] private GameObject TextObject;
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

        timerText = TextObject.GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        //GameEvents.OnTimerPause += PauseTimer;
        GameEvents.OnTimerRanOut += ResetTimer;
        //GameEvents.OnQuotaSet += NewTimer;
    }

    private void OnDisable()
    {
        //GameEvents.OnTimerPause -= PauseTimer;
        //GameEvents.OnQuotaSet -= NewTimer;
        GameEvents.OnTimerRanOut -= ResetTimer;
    }

    void Update()
    {
        if (candleOn)
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
                Debug.Log("TimeLeft: " + timeLeft);
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
         candleOn = false;
    }

    public void ResumeTimer()
    {
        candleOn = true;
        timeSinceLastStep = 0;
    }

    private void ResetTimer()
    {
        timeLeft = 0f;
        AddTime(timeMax);
    }

    // ----------------------- SWITCH TIMER ON NEW LEVEL -------------------------
    public void NewTimer()
    {
        //value not used, timer duration used from the array instead
        if (timerIndex < timerArray.Length)
        {
            timeMax = timerArray[timerIndex];
            AddTime(timeMax);
            candleOn = true;
        }
        timerIndex++;
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
}