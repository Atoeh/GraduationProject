using System;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    private float quota;
    public float lootAmmount = 0;
    public bool quotaIsTreu = false;

    void Start()
    {
        LootPickUp[] allLoot = FindObjectsByType<LootPickUp>(FindObjectsSortMode.None);
        foreach (LootPickUp loot in allLoot)
        {
            quota += loot.lootValue;
        }
        if (quotaIsTreu == true)
        {
            GameEvents.QuotaSet(quota);
            Debug.Log("quota is " + quota);
            Debug.Log("Length is " + allLoot.Length.ToString());
            ResetLoot();
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

}
