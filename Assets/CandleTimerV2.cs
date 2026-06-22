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
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private float updateTime = 3f;


    //[SerializeField]
    //private float timeStart = 50f;
    private float timeSinceLastStep = 0f;

    private float timeMax;

    private bool timerRestarted;


    [Header("- Timer array -")]

    [SerializeField]
    private float[] timerArray;
    //[SerializeField]
    private int timerIndex = 0;

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
        
        //timeStart = timerArray[0];
        //timeLeft = timeStart;
        //timeLeft = timerArray[0];
        //timeMax = timerArray[0];
        //SetSlider();

        //timerIndex = 0;
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
        if ((timeLeft + timeAdded) < timeMax)
        {
            timeLeft += timeAdded;
        }
        else
        {
            Debug.Log("timeleft more then timeMax?");
            timeLeft = timeMax;
        }
        SetSlider();
        timerRestarted = false;
    }

    private void SetSlider()
    {
        Debug.Log("timeLeft = " + timeLeft + " timeMax = " + timeMax);
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
        Debug.Log("The timer should reset here");
        AddTime(timeMax);
        PauseTimer();
    }

    // ----------------------- SWITCH TIMER ON QUOTA -------------------------

    private void NewTimer(float value)
    {
        //code that changes the timer, value not used
        //Debug.Log("timerIndex = " + timerIndex);
        if (timerIndex < timerArray.Length)
        {
            timeMax = timerArray[timerIndex];
            //als timeAdded meer dan timeMax is > timeLeft = timeMax.
            AddTime(timeMax);
        }

        timerIndex++;

        PauseTimer();
    }
}
