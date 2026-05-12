using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosePopUpScript : MonoBehaviour
{
    private GameObject mainTrigger;

    public void Start()
    {
        mainTrigger = GameObject.Find("TriggerN (Main)");
    }

    public void CloseUI()
    {
        Debug.Log("CloseUI");
        GameEvents.ToggleMoveUI();
        mainTrigger.GetComponent<MainTriggerScript>().Exit();
        Destroy(gameObject);
    }
}
