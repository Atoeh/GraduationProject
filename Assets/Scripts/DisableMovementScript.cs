using System;
using UnityEngine;

public class DisableMovementScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private GameObject[] trigger;

    public void LockTriggers()
    {
        for (int i = 0; i < trigger.Length; i++)
        {
            trigger[i].GetComponent<MovementTriggerScript>().MovementLockToggle();
        }
    }

}
