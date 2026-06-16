using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorScriptV2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.TimerPause();
            GameEvents.OpenDoor();
        }
    }
}
