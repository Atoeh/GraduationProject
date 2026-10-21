using System.Security.Cryptography.X509Certificates;
using UnityEngine;

//Wat doet de deur?
//1 Deur is niet interactief (later een popup dat je quota moet hitten voordat je er weer uit mag)
//2 Speler heeft quota (event) gehaald (later een popup dat de deur geopend is)
//3 Deur is interactief
//4 Wann met de deur geinteracteerd wordt wordt van scenegeswitched

public class DoorScriptV2 : MonoBehaviour
{
    private bool doorOpen;

    [SerializeField] private GameObject parentObj;
    [SerializeField] private GameObject cutScene1;
    [SerializeField] private GameObject cutScene2;
    [SerializeField] private Transform hudTransform;

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
    }

    private void OpenDoor()
    {
        doorOpen = true;
    }

    private void CloseDoor()
    {
        doorOpen = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            if (doorOpen)
            {
                Debug.Log("Collided with door + quota met = Open the door");
                if (cutScene2 != null)
                {
                    UIManagerScriptV4.Instance.StartCutScene(cutScene2, null);
                    Debug.Log("Instantiate cutscene 2");
                }
                Destroy(parentObj);
            }
            else
            {
                Debug.Log("Collided with door + quota not met = popup");
                UIManagerScriptV4.Instance.StartCutScene(cutScene1, null);
            }
        }
    }
}