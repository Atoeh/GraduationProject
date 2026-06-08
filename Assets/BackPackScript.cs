using UnityEngine;

public class BackPackScript : MonoBehaviour
{
    [SerializeField]
    private bool introDone = false;

    [SerializeField]
    private GameObject introLine;
    [SerializeField]
    private GameObject counterLine;
    [SerializeField]
    private GameObject firstLootLine;

    [SerializeField]
    private bool isMunching = false;
    [SerializeField]
    private GameObject munchText;
    [SerializeField]
    private float maxTime;
    private float startTime;

    private void OnEnable()
    {
        GameEvents.OnLootPickUp += OpenDialogue;
        GameEvents.OnLootPickUp += MunchDown;
    }

    private void OnDisable()
    {
        GameEvents.OnLootPickUp -= OpenDialogue;
        GameEvents.OnLootPickUp -= MunchDown;
    }

    private void Start()
    {
        introDone = false;
        isMunching = false;
        munchText.SetActive(false);
        //popUpLootText = popUpObj.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (isMunching == true)
        {
            munchText.SetActive(true);

            if (Time.time - startTime >= maxTime)
                isMunching = false;
        }
        else
            munchText.SetActive(false);
    }

    private void OpenDialogue(float value)
    {
        if (!introDone)
        {
            introLine.SetActive(true);
            introDone = true;
        }
        else
        { 
            //firstLootLine.SetActive(false);
        }
    }

    private void MunchDown(float value)
    {
        startTime = Time.time;
        isMunching = true;
    }
}

    //In inventory script => if loot pickUP => GO to hand
    // Go to hand = value aan hand, niks anders oprapen

    //On DaggerClicked => Event DaggerDragging && Event toggle movement
    //Event Dagger dragging => alleen nog kunnen klikken op hand (om terug te leggen) of scherm + object op cursor is Dagger
    //als speler op scherm clicked => Attack event
    //Attack event => sound effect mes + en als (enemy in bepaalde range) = neemt damage & sound effect
    //DaggerClicked en dagger dragging still active => dagger back to hand

    //If loot in hand enable raycast target?
    //On LootClicked => Event LootDragging && Event toggle movement
    //Event LootDragging => Backpack ogen open + alleen nog kunnen klikken op hand (om terug te leggen) of backpack + object op cursor is Loot bag
    //Als speler op Backpack klikt dan verdwijnt de lootbag en ben je en is lootDragging over
    //Backpack geklikt betekent dat je 

    //Voor nu is dit te veel, wat is een makkelijkere manier om dit te doen???

    //wanneer event van value changed wordt gedaan voor eerste keer tekst popup, eet animatie
