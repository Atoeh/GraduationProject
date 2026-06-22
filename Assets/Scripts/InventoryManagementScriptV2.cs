using System;
using Unity.Jobs;
using UnityEngine;

public class InventoryManagementScriptV2 : MonoBehaviour
{
    [Header("- Loot variables -")]

    private float quota;
    public float lootAmmount = 0;
    private bool lootless = false;

    [Header("- Quota variables -")]

    public bool quotaIsTreu = false;
    [SerializeField]
    private bool quotaIsAll = false;

    [Header ("- Quota Array variables -")]

    [SerializeField]
    private float[] quotaArray;
    [SerializeField]
    private int quotaIndex;

    void Start()
    {
        if (quotaIsTreu == true)
        {
            if (quotaIsAll == true)
            {
                QuotaIsAll();
            }
            else
            {
                quotaIndex = 0;
                quota = quotaArray[quotaIndex];
                UpdateQuota(quota);
                quotaIndex++;
            }
        }
    }

    private void OnEnable()
    {
        GameEvents.OnLootPickUp += LootPickUp;
    }

    private void OnDisable()
    {
        GameEvents.OnLootPickUp -= LootPickUp;
    }

    //Called NoLoot niet meer zit nu in de LootUi
    private void LootPickUp(float value, GameObject image)
    {
        //verrander de waarde van curr loot in de quota dinges
        //Wacht moet dat hier of moet ik daar nog een ander script voor maken omdat dit een manager is?
        lootAmmount += value;
        GameEvents.InventoryUpdated(lootAmmount);

        if (lootAmmount >= quota)
        {
            //Debug.Log("QuotaHit");
            ReachedQuota();
            GameEvents.QuotaHit();
            ResetLoot();
        }

        if (lootAmmount <= 0 && value < 0)
        {
            GameEvents.NoLoot();
            Debug.Log("NoLoot called");
            ResetLoot() ;
        }
    }

    private void ResetLoot()
    {
        lootAmmount = 0f;
        GameEvents.InventoryUpdated(lootAmmount);
    }

    public void UpdateQuota(float value)
    {
        GameEvents.QuotaSet(value);
    }

    public void QuotaIsAll()
    {
        LootPickUp[] allLoot = FindObjectsByType<LootPickUp>(FindObjectsSortMode.None);
        foreach (LootPickUp loot in allLoot)
        {
            quota += loot.lootValue;
        }

        UpdateQuota(quota);
    }

    public void ReachedQuota()
    {
        if (quotaIsAll == false)
        {
            //als alle quotas nog niet gehaald zijn verplaats speler met volgende quota
            if (quotaIndex < quotaArray.Length)
            {
                ResetLoot();
                quota = quotaArray[quotaIndex];
                UpdateQuota(quota);
                quotaIndex++;
            }
            else
            {
                LastQuotaReached();
            }
        }
        else
            LastQuotaReached();
    }

//Should trigger the final cutscene
    public void LastQuotaReached()
    {
        if (quotaIsTreu == true)
        {
            Debug.Log("All quotas have been completed should now trigger the endcutscene");
        }
    }
}
