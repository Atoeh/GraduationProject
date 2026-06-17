using System;
using UnityEngine;

public class InventoryManagementScriptV2 : MonoBehaviour
{
    [SerializeField]
    private float quota;
    public float lootAmmount = 0;
    public bool quotaIsTreu = false;
    [SerializeField]
    private bool quotaIsAll = false;

    void Start()
    {
        if (quotaIsAll == true)
        {
            QuotaIsAll();
        }
        //ResetLoot();
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
            Debug.Log("QuotaHit");
            GameEvents.QuotaHit();
        }

        if (lootAmmount <= 0)
        {
            GameEvents.NoLoot();
            Debug.Log("NoLoot called");
        }
    }

    private void ResetLoot()
    {
        lootAmmount = 0f;
        GameEvents.InventoryUpdated(lootAmmount);
    }

    public void UpdateQuota()
    {
        GameEvents.QuotaSet(quota);
    }

    public void QuotaIsAll()
    {
        LootPickUp[] allLoot = FindObjectsByType<LootPickUp>(FindObjectsSortMode.None);
        foreach (LootPickUp loot in allLoot)
        {
            quota += loot.lootValue;
        }

        UpdateQuota();

        //Debug.Log("quota is " + quota);
        //Debug.Log("Length is " + allLoot.Length.ToString());
    }
}
