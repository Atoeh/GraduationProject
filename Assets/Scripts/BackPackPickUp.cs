using UnityEngine;

public class BackPackPickUP : MonoBehaviour
{
    public GameObject visual;
    private Transform canvas;
    [SerializeField]
    private GameObject backPackUI;
    [SerializeField]
    private GameObject canvasVisual;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.maroon;
        Gizmos.DrawSphere(transform.position, .5f);
    }

    private void Start()
    {
        backPackUI.SetActive(false);
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
            EnableBackPack();
            Destroy(this.gameObject);
        }
    }

    private void EnableBackPack()
    {
        backPackUI.SetActive(true);
        GameEvents.QuotaSet(12f);
        //GameEvents.LootPickUp(0f, canvasVisual);
    }
}