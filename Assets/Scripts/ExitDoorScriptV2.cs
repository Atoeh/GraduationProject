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
    [SerializeField] private GameObject ExitText;

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += OpenDoor;
        GameEvents.OnQuotaLost += CloseDoor;
    }
    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= OpenDoor;
        GameEvents.OnQuotaLost -= CloseDoor;
    }

    private void Start()
    {
        doorOpen = false;
        ExitText.SetActive(doorOpen);
        doorBorder = this.gameObject;
    }

    private void OpenDoor()
    {
        doorOpen = true;
        Debug.Log("Door opens");
        ExitText.SetActive(doorOpen);
    }

    private void CloseDoor()
    {
        Debug.Log("Door closes");
        doorOpen = false;
        ExitText.SetActive(doorOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && doorOpen == true)
        {
            Debug.Log("Collision with door");
            GameEvents.ExitLevel();
        }
    }
}