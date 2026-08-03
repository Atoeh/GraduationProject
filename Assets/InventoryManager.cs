using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] private GridManager gridManager;

    private ItemData heldItem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PickupItem(ItemData itemData)
    {
        // Ignore if already holding an item
        if (heldItem != null)
        {
            Debug.Log("Already holding an item");
            return;
        }

        heldItem = itemData;
        gridManager.SpawnHeldItem(itemData);
    }

    public void ClearHeldItem()
    {
        heldItem = null;
    }
}