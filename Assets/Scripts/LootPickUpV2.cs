using UnityEngine;

public class LootPickUpV2 : MonoBehaviour
{
    [SerializeField]
    private float valueMin;
    [SerializeField]
    private float valueMax;
    public float lootValue;

    public GameObject visual;
    public GameObject canvasVisual;
    private Transform canvas;

    private void OnEnable()
    {
        GameEvents.OnNoLoot += UnInteractable;
    }
    private void OnDisable()
    {
        GameEvents.OnNoLoot -= UnInteractable;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.maroon;
        Gizmos.DrawSphere(transform.position, .5f);
    }

    private void Start()
    {
        lootValue = Mathf.Round(Random.Range(valueMin, valueMax));

        canvas = GetComponentInChildren<Canvas>().transform;
        if (visual != null)
        {
            Instantiate(visual, canvas.position, canvas.rotation, canvas);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameEvents.LootPickUp(lootValue, canvasVisual);
            Destroy(this.gameObject);
        }
    }

    private void UnInteractable()
    {
        //Destroys the scipt not the object
        Destroy(this);
    }
}
