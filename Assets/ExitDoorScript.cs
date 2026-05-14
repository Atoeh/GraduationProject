using System.Security.Cryptography.X509Certificates;
using UnityEngine;

//Wat doet de deur?
//1 Deur is niet interactief (later een popup dat je quota moet hitten voordat je er weer uit mag)
//2 Speler heeft quota (event) gehaald (later een popup dat de deur geopend is)
//3 Deur is interactief
//4 Wann met de deur geinteracteerd wordt wordt van scenegeswitched

public class ExitDoorScript : MonoBehaviour
{
    private bool isOpen = false;

    private void Start()
    {
        isOpen = false;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isOpen == true)
            GameEvents.OpenDoor();
    }

}
