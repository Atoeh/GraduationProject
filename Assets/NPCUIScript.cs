using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCUIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject otherButton;
    [SerializeField]
    private GameObject introObj;
    [SerializeField]
    private GameObject choiceObj;
    [SerializeField]
    private float DropAmm  = -20f;
    [SerializeField] 
    private float StealAmm = 240;
    public GameObject npcBody;

    private void OnEnable()
    {
        GameEvents.OnNoLoot += EnableButton;
        GameEvents.OnNoLoot += DisAbleOtherButton;
    }

    private void OnDisable()
    {
        GameEvents.OnNoLoot -= EnableButton;
        GameEvents.OnNoLoot -= DisAbleOtherButton;

    }

    private void Start()
    {
        npcBody = FindFirstObjectByType<NPCScript>().gameObject;
        introObj.SetActive(true);
        choiceObj.SetActive(false); 
    }

    public void NextButton()
    {
        introObj.SetActive(false);
        choiceObj.SetActive(true);
        button.GetComponent<Button>().interactable = false;
    }

    public void DropLoot()
    {
        GameEvents.LootPickUp(DropAmm);
    }

    public void PickUpPerson()
    {
        GameEvents.QuotaHit();
        npcBody.SetActive(false);
        ClosePopUP();
    }

    public void StealLoot()
    {
        GameEvents.LootPickUp(StealAmm);
        ClosePopUP();
    }

    private void EnableButton()
    { 
        button.GetComponent<Button>().interactable = true;
    }

    private void DisAbleOtherButton()
    {
        otherButton.GetComponent<Button>().interactable = false;
    }

    private void ClosePopUP()
    {
        GameEvents.ToggleMoveUI();
        GameEvents.TimerPause();
        Destroy(gameObject);
    }
}
