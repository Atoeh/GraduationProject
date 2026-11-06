using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

//Wat doet de deur?
//1 Deur is niet interactief (later een popup dat je quota moet hitten voordat je er weer uit mag)
//2 Speler heeft quota (event) gehaald (later een popup dat de deur geopend is)
//3 Deur is interactief
//4 Wann met de deur geinteracteerd wordt wordt van scenegeswitched

public class ExitDoorScriptV2 : MonoBehaviour
{
    private bool doorOpen = false;
    private bool pickedUpNPC = false;
    //private bool doorLocked = false;

    private GameObject doorBorder;
    //[SerializeField] private GameObject exitText;

    [SerializeField] private GameObject exitText;
    [SerializeField] private GameObject cutScene;
    [SerializeField] private Transform hudTransform;

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += OpenDoor;
        GameEvents.OnQuotaLost += CloseDoor;

        GameEvents.OnNPCPickedup += NPCPickedUP;
    }
    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= OpenDoor;
        GameEvents.OnQuotaLost -= CloseDoor;

        GameEvents.OnNPCPickedup -= OpenDoor;
    }

    private void Start()
    {
        doorOpen = false;
        exitText.SetActive(doorOpen);
        doorBorder = this.gameObject;
    }

    private void OpenDoor()
    {
        doorOpen = true;
        Debug.Log("Door opens");
        //exitText.SetActive(true);
    }

    private void CloseDoor()
    {
        if (!pickedUpNPC)
        {
            Debug.Log("Door closes");
            doorOpen = false;
            exitText.SetActive(false);
        }
    }

    private void NPCPickedUP()
    {
        Debug.Log("Door permanantly open, NPC pickedUp");
        pickedUpNPC = true;
        OpenDoor();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (doorOpen)
            {
                if (pickedUpNPC)
                {
                    Debug.Log("Entered Door + NPC = GoodEnding");
                    PlayCutscene();
                }
                else
                {
                    Debug.Log("Entered Door - NPC = BadEnding");
                    GameEvents.ExitLevel();
                }
            }
        }
    }

    private void PlayCutscene() => UIManagerScriptV4.Instance.StartCutScene(cutScene, null);
}