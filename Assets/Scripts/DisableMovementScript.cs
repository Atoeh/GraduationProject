using System;
using UnityEngine;

public class DisableMovementScript : MonoBehaviour
{
    [SerializeField]

    public void DisableMovement()
    {
        GameEvents.ToggleMoveUI();
    }
}
