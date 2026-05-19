using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //Doel van script is dat deuren na bepaalde hoeveelheid aan loot open gaan of op hitten van de quota
    [SerializeField]
    bool openOnQuota = false;
    [SerializeField]
    float entryAmmount;
    float currAmm;

    private void OnEnable()
    {
        GameEvents.OnLootPickUp += CheckAmmount;
        GameEvents.OnQuotaHit += QuotaHit;
    }

    private void OnDisable()
    {
        GameEvents.OnLootPickUp -= CheckAmmount;
        GameEvents.OnQuotaHit -= QuotaHit;
    }

    private void CheckAmmount(float value)
    {
        if (openOnQuota == false)
        {
            currAmm += value;
            if (currAmm >= entryAmmount)
                OpenDoor();
        }
    }

    private void QuotaHit()
    {
        if (openOnQuota == true)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        Debug.Log("Door Opend");
        this.gameObject.SetActive(false);
    }
}
