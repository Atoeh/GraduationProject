using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    public float lootValue;
    public GameObject visual;
    [SerializeField]
    private GameObject canvas;

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
        if (visual != null)
        {
            Transform canv = canvas.transform;
            Instantiate(visual, canv.position, canv.rotation, canv);
        }
    }

    private void UnInteractable()
    {
        Destroy(this);
    }
}
