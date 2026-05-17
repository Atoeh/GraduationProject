using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class IncreaseCandleScript : MonoBehaviour
{
    [SerializeField]
    private ChangeLightingScript lightingScript;
    [SerializeField]
    private float value;

    private void OnEnable()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        lightingScript = canvas.GetComponentInChildren <ChangeLightingScript>();
    }

    public void AddCandleTime()
    {
        lightingScript.ChangeCandleAmmount(value);
    }
}
