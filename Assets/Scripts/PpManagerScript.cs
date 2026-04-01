using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PpManagerScript : MonoBehaviour
{
    private Volume volume;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    private void OnEnable()
    {
        GameEvents.OnChangeDarkness += CandleTrigger;
    }

    private void OnDisable()
    {
        GameEvents.OnChangeDarkness -= CandleTrigger;
    }

    void Start()
    {
        volume = this.GetComponent<Volume>();
        // Try to get the Vignette override from the Volume
        if (volume.profile.TryGet(out vignette))
        {
            // Enable override if not already
            vignette.intensity.overrideState = true;
        }

        if (volume.profile.TryGet(out colorAdjustments))
        { 
            colorAdjustments.postExposure.overrideState = true;
        }
    }

    public void ChangeVignette(float value)
    {
        if (vignette != null)
        {
            vignette.intensity.value = value;
        }
    }

    public void ChangeToneMapping(float value)
    {
        if (colorAdjustments != null)
        { 
            colorAdjustments.postExposure.value = value;
        }
    }

    void CandleTrigger(float oldValue)
    {
        float value = oldValue / 2;
        //ChangeVignette(value);
        ChangeToneMapping(value);
    }
}
