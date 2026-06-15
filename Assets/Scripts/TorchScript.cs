using System.Threading;
using UnityEngine;

public class TorchScript : MonoBehaviour
{
    [SerializeField]
    private float minIntensity;
    private float midIntensity;
    [SerializeField]
    private float maxIntensity;

    private int stateInt;
    private int maxState = 4;

    private float prevTime;
    [SerializeField]
    private float deltaTime;

    private Light lightSource;

    void Start()
    {
        lightSource = GetComponentInChildren<Light>();
        lightSource.intensity = minIntensity;
        midIntensity = (minIntensity + maxIntensity) / 2f;
        stateInt = Random.Range(0, maxState + 1);
        prevTime = Time.time;
    }

    void Update()
    {
        if(Time.time - prevTime >= deltaTime)
        {
            stateInt = (stateInt + 1) % maxState;
            prevTime = Time.time;
            ChangeLightIntensity();
        }
    }

    private void ChangeLightIntensity()
    {
        if (stateInt == 0)
            lightSource.intensity = midIntensity;
        if (stateInt == 1)
            lightSource.intensity = maxIntensity;
        if (stateInt == 2)
            lightSource.intensity = midIntensity;
        if (stateInt == 3)
            lightSource.intensity = maxIntensity;
        if (stateInt == 4)
            lightSource.intensity = minIntensity;
    }
}
