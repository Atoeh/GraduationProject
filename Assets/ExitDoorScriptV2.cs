using UnityEngine;

public class ExitDoorScriptV2 : MonoBehaviour
{
    private bool isOpen = false;
    [SerializeField]
    private GameObject doorBorder;
    [SerializeField]
    private GameObject ExitText;

    private void Start()
    {
        isOpen = false;
        ExitText.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += UnlockExit;
    }
    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= UnlockExit;
    }

    private void UnlockExit()
    {
        isOpen = true;
        doorBorder.SetActive(false);
        ExitText.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isOpen == true)
        {
            GameEvents.TimerPause();
            GameEvents.OpenDoor();
            Debug.Log("DoorOpened triggered");
        }
    }
}