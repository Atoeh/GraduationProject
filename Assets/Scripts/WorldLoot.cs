using UnityEngine;

public class WorldLoot : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.PickupItem(itemData);
            Destroy(gameObject);
        }
    }
}