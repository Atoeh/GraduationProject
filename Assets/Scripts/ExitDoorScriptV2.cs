using System.Security.Cryptography.X509Certificates;
using UnityEngine;

//Wat doet de deur?
//1 Deur is niet interactief (later een popup dat je quota moet hitten voordat je er weer uit mag)
//2 Speler heeft quota (event) gehaald (later een popup dat de deur geopend is)
//3 Deur is interactief
//4 Wann met de deur geinteracteerd wordt wordt van scenegeswitched

public class ExitDoorScriptV2 : MonoBehaviour
{
    private bool doorOpen = false;
    //private bool doorLocked = false;

    private GameObject doorBorder;
    [SerializeField]
    private GameObject ExitText;

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += OpenDoor;
        GameEvents.OnQuotaLost += CloseDoor;
    }
    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= OpenDoor;
        GameEvents.OnQuotaLost += CloseDoor;
    }

    private void Start()
    {
        doorOpen = false;
        ExitText.SetActive(false);
        doorBorder = this.gameObject;
    }

    private void OpenDoor()
    {
        doorOpen = true;
        ExitText.SetActive(true);
    }

    private void CloseDoor()
    {
        doorOpen = false;
        ExitText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with door");

        if (other.tag == "Player" && doorOpen == true)
        {
            //voor nu met bool doorgeven omdat..
            GameEvents.TimerPause();
            GameEvents.ExitLevel();
            Debug.Log("DoorOpened triggered");
        }
    }
}