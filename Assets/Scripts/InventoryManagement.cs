using System;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    [SerializeField]
    private float quota;
    public float lootAmmount = 0;

    void Start()
    {
        GameEvents.QuotaSet(quota);
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
