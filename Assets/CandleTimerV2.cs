using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CandleTimerV2 : MonoBehaviour
{
    [Header("- Timer array -")]

    [SerializeField]
    private float timeLeft = 0f;
    public bool candleOn = true;
    private Slider slider;

    [SerializeField]
    private float updateTime = 3f;

    [SerializeField]
    private float timeMax = 60f;

    [SerializeField]
    private float timeStart = 50f;
    private float timeSinceLastStep = 0f;

    private bool timerRestarted;

    [Header("- Timer array -")]

    [SerializeField]
    private float[] timerArray;
    [SerializeField]
    private int timerIndex;

    private void OnEnable()
    {
        GameEvents.OnTimerPause += PauseTimer;
        GameEvents.OnQuotaSet += NewTimer;
        GameEvents.OnCandleDepleted += ResetTimer;
    }

    private void OnDisable()
    {
        GameEvents.OnTimerPause -= PauseTimer;
        GameEvents.OnQuotaSet -= NewTimer;
        GameEvents.OnCandleDepleted -= ResetTimer;
    }

    void Start()
    {
        candleOn = false;
        slider = GetComponent<Slider>();
        timeLeft = timeStart;
        SetSlider();

        timerIndex = -1;
        timerRestarted = false;
    }

    void Update()
    {
        if (candleOn == true)
        {
            //Is there still time left?
            if (timeLeft <= 0f && timerRestarted == false)
            {
                timerRestarted = true;
                timeLeft = 0f;
                GameEvents.CandleDepleted();
            }

            //if timeSincelastStep is larger then a second, subtract second from timer.
            timeSinceLastStep += Time.deltaTime;
            if (timeSinceLastStep >= updateTime)
            {
                timeLeft = timeLeft - updateTime;
                SetSlider();
                timeSinceLastStep = 0;
            }
        }
    }

    // ----------------------- STANDARD TIMER -------------------------

    public void AddTime(float timeAdded)
    {
        candleOn = true;

        //add time to timer but no more than the timeMax
        if ((timeLeft + timeAdded) <= timeMax)
        {
            timeLeft += timeAdded;
        }
        else timeLeft = timeMax;
        SetSlider();
        timerRestarted = false;

        Debug.Log("added time, timeLeft = " + timeLeft);
    }

    private void SetSlider()
    {
        slider.value = timeLeft / timeMax;
    }

    public void PauseTimer()
    {
        if (candleOn == true)
        {
            candleOn = false;
        }
        else
        {
            candleOn = true;
            timeSinceLastStep = 0;
        }
    }

    private void ResetTimer()
    {
        //Resets the timer when the timer runs out
        Debug.Log("The timer should reset here");
        AddTime(timeMax);
        //PauseTimer();
    }

    // ----------------------- SWITCH TIMER ON QUOTA -------------------------

    private void NewTimer(float value)
    {
        //code that changes the timer, value not used
        timerIndex++;
        if (timerIndex < timerArray.Length)
        {
            timeMax = timerArray[timerIndex];
            //als timeAdded meer dan timeMax is > timeLeft = timeMax.
            AddTime(timeMax);
        }else
            Debug.Log("timerIndex is higher than timer array, timerIndex = " + timerIndex);
        PauseTimer();
    }
}
