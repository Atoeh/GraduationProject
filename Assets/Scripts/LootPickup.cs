using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    public float lootValue;
    [SerializeField]
    private GameObject visual;

    private void OnEnable()
    {
        GameEvents.OnNoLoot += UnInteractable;
    }
    private void OnDisable()
    {
        GameEvents.OnNoLoot -= UnInteractable;
    }

    private void Start()
    {
        InstantiateVisual();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gold;
        Gizmos.DrawSphere(transform.position , .5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.LootPickUp(lootValue);
            //Fucntie toevoegen die particle effect spawnt op loot collectie
            Destroy(this.gameObject);
        }
    }

    private void InstantiateVisual()
    {
        if(visual != null)
            Instantiate(visual, transform.position, transform.rotation, transform);
    }

    private void UnInteractable()
    {
        Destroy(this);
    }
}
