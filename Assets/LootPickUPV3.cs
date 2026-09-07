using System.Xml.Serialization;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class LootPickUpV3 : MonoBehaviour
{
    public ItemData itemData;
    public Sprite sprite;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.maroon;
        Gizmos.DrawSphere(transform.position, .5f);
    }

    private void Start()
    {
        sprite = itemData.sprite;

        if (sprite != null)
        {
            GameObject visual = new GameObject("Visual");
            visual.transform.SetParent(transform);
            visual.transform.localPosition = new Vector3(0 ,.5f ,0);
            visual.transform.localScale = new Vector3(2 ,2 ,1);

            SpriteRenderer spriteRenderer = visual.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemData.sprite;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            Debug.Log("Collided with player");
            InventoryManager.Instance.PickupItem(itemData);
            Destroy(this.gameObject);
        }
    }
}
