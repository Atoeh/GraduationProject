using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CandleTimer : MonoBehaviour
{
    //Perhaps this should be linked to movement and not to actual time?

    [SerializeField]
    public static float timeLeft = 0f;
    public bool candleOn = true;
    private Slider slider;

    [SerializeField]
    private float updateTime = 3f;

    [SerializeField]
    private float timeMax = 60f;

    [SerializeField]
    private float timeStart = 50f;
    private float timeSinceLastStep = 0f;

    private void OnEnable()
    {
        GameEvents.OnTimerPause += PauseTimer;
    }

    private void OnDisable()
    {
        GameEvents.OnTimerPause -= PauseTimer;
    }

    void Start()
    {
        candleOn = false;
        slider = GetComponent<Slider>();
        timeLeft = timeStart;
        SetSlider();
    }

    void Update()
    {
        if (candleOn == true)
        {
            //Is there still time left?
            if (timeLeft <= 0f)
            {
                GameEvents.CandleDepleted();
                timeLeft = 0f;
                candleOn = false;
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

    /// <summary>
    /// function that adds a certain ammount of time to the timer / candle
    /// </summary>
    /// <param name="timeAdded"> the ammount of time that will be added to the timer/ candle. </param>
    public void AddTime(float timeAdded)
    {
        candleOn = true;

        //add time to timer but no more than the timeMax
        if ((timeLeft + timeAdded) <= timeMax)
        { 
            timeLeft += timeAdded; 
        }else timeLeft = timeMax;
        SetSlider();
        Debug.Log("added time, timeLest = " + timeLeft);
    }

    private void SetSlider()
    {
        slider.value = timeLeft/timeMax;
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

}
