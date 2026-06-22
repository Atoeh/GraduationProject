using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField]
    private GameObject uiElement;
    [SerializeField]
    private GameObject spawnPoint;

    //OP player on trigger wnter
    //Pause timer ++ open via uiManager uielement

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    //pauze en open ui
        //    GameEvents.TimerPause();
        //    GameEvents.ToggleMoveUI();
        //    Instantiate(uiElement, spawnPoint.transform);
        //    uiElement.GetComponent<NPCUIScript>().npcBody = gameObject;
        //}
    }
}
