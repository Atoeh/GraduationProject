using System;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    private float quota;
    public float lootAmmount = 0;

    void Start()
    {
        LootPickUp[] allLoot = FindObjectsByType<LootPickUp>(FindObjectsSortMode.None);
        foreach (LootPickUp loot in allLoot)
        {
            quota += loot.lootValue;
        }
        GameEvents.QuotaSet(quota);
        Debug.Log("quota is " + quota);
        Debug.Log("Length is " + allLoot.Length.ToString());
        ResetLoot();
    }

    private void OnEnable()
    {
        GameEvents.OnLootPickUp += LootPickUp;
    }

    private void OnDisable()
    {
        GameEvents.OnLootPickUp -= LootPickUp;   
    }

    private void LootPickUp(float value)
    {
        //verrander de waarde van curr loot in de quota dinges
        //Wacht moet dat hier of moet ik daar nog een ander script voor maken omdat dit een manager is?
        lootAmmount += value;
        GameEvents.InventoryUpdated(lootAmmount);

        if (lootAmmount >= quota)
        {
            GameEvents.QuotaHit();
        }
    }

    private void ResetLoot()
    {
        lootAmmount = 0f;
        GameEvents.InventoryUpdated(lootAmmount);
    }

}
