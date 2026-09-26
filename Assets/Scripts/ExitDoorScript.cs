using System.Security.Cryptography.X509Certificates;
using UnityEngine;

//Wat doet de deur?
//1 Deur is niet interactief (later een popup dat je quota moet hitten voordat je er weer uit mag)
//2 Speler heeft quota (event) gehaald (later een popup dat de deur geopend is)
//3 Deur is interactief
//4 Wann met de deur geinteracteerd wordt wordt van scenegeswitched

public class ExitDoorScript : MonoBehaviour
{
    private bool doorOpen = false;
    private bool doorLocked = false;

    [SerializeField]
    private GameObject doorBorder;
    [SerializeField]
    private GameObject ExitText;

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += OpenDoor;
    }
    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= OpenDoor;
    }

    private void Start()
    {
        doorOpen = false;
        ExitText.SetActive(false);
    }

    private void OpenDoor()
    {
        doorOpen = true;
        doorBorder.SetActive(false);
        ExitText.SetActive(true);  
    }

    private void CloseDoor()
    {
        doorOpen = false;
        doorBorder.SetActive(true);
        ExitText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && doorOpen == true)
        {
            //voor nu met bool doorgeven omdat..
            GameEvents.TimerPause();
            GameEvents.ExitLevel();
            Debug.Log("DoorOpened triggered");
        }
    }
}