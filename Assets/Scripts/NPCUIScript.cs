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
    private float StealAmm = 240;
    public GameObject npcBody;
    [SerializeField]
    private GameObject lootImage;

    //-------------------------------DROPPING Sequence
    //[SerializeField]
    //private float dropAmm = -20f;
    //[SerializeField]
    //private float dropped = 0f;
    //[SerializeField]
    //private float dropMax = 40f;

    [SerializeField]
    private GameObject comment1;
    [SerializeField]
    private GameObject comment2;
    [SerializeField]
    private GameObject blocked;
    [SerializeField]

    //-------------------------------

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
        //dropped = 0f;
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
        //dropped += dropAmm;
        //if (dropped >= dropMax && dropped <= 2*dropMax)
        //{
        //    Debug.Log("backpackie comment 1");
        //    comment1.SetActive(true);
        //}
        //if (dropped >= 2 * dropMax)
        //{
        //    Debug.Log("Backpackie comment 2");
        //    comment2.SetActive(true);
        //}
        //if (dropped > 3 * dropMax)
        //{
        //    Debug.Log("Backpackie Blocking you");
        //    blocked.SetActive(true);
        //}
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
