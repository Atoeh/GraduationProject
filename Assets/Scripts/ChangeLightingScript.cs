using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/// <summary>
/// Code on the UICandle that makes environment darker based on the candle ammount left
/// when slider value change, executes pp changes in the PP manager of the global volume
/// </summary>
public class ChangeLightingScript : MonoBehaviour
{
    //UIManager
    [SerializeField]
    UIManagerScript uiManager;

    //GLobal Volume stuff
    public Volume volume;

    //Candle values
    [SerializeField]
    bool candleOn = true;
    float candleAmmount;

    private void OnEnable()
    {
        candleAmmount = this.GetComponent<Slider>().value;
    }

    public void CheckCandleAmmount()
    {
        candleAmmount = this.GetComponent<Slider>().value;
    }

    public void ChangeCandleAmmount(float value)
    { 
        CheckCandleAmmount();
        this.GetComponent<Slider>().value = candleAmmount + value;
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
