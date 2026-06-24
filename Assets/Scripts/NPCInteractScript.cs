using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractScript : MonoBehaviour
{
    [Header ("- Buttons -")]

    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject otherButton;
    [SerializeField]
    private GameObject introObj;
    [SerializeField]
    private GameObject choiceObj;

    [SerializeField]
    private float StealAmm = 240;
    public GameObject npcBody;
    [SerializeField]
    private GameObject lootImage;

    [SerializeField]
    private GameObject ending1;
    [SerializeField]
    private GameObject ending2;
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
        GameEvents.DropLoot();
    }

    public void PickUpPerson()
    {
        GameEvents.QuotaHit();
        npcBody.SetActive(false);
        ClosePopUP();
    }

    public void StealLoot()
    {
        GameEvents.LootPickUp(StealAmm, lootImage);
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
