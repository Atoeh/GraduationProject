using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

//Mag later in de game manager worden geplaatst die allemaal events triggered voor de UIManager
/// <summary>
/// Code on the UICandle that makes environment darker based on the candle ammount left
/// when slider value change, executes pp changes in the PP manager of the global volume
/// </summary>
public class ChangeLightingScript : MonoBehaviour
{
    //UIManager
    [SerializeField]
    private GameObject uiManagerHolder;
    UIManagerScript uiManager;

    //GLobal Volume stuff
    //public Volume volume;

    //Candle values
    [SerializeField]
    bool candleOn = true;
    float candleAmmount;

    private void OnEnable()
    {
        candleAmmount = this.GetComponent<Slider>().value;
    }

    private void Start()
    {
        uiManager = uiManagerHolder.GetComponent<UIManagerScript>();
    }

    public void CheckCandleAmmount()
    {
        candleAmmount = this.GetComponent<Slider>().value;
    }

    public void ChangeCandleAmmount(float value)
    { 
        CheckCandleAmmount();
        CandleTimer.timeLeft = CandleTimer.timeLeft + value;
        ChangeDarkness();
    }

    public void ChangeDarkness()
    {
        CheckCandleAmmount();
        float value = 1 - candleAmmount;
        //GameEvents.ChangeDarkness(candleAmmount);
        uiManager.ChangeDarkness(value);
        //volume.GetComponent<PpManagerScript>().ChangeLightning(value);
    }

    public void InvokeDarkness()
    {
        CheckCandleAmmount();
        float value = 1 - candleAmmount;
        GameEvents.ChangeDarkness(value);
    }
}
