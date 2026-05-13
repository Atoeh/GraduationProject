using UnityEngine;

public class LootPickUp : MonoBehaviour
{
    [SerializeField]
    private float lootValue;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Loot Picked UP");
            GameEvents.LootPickUp(lootValue);
            //Fucntie toevoegen die particle effect spawnt op loot collectie
            Destroy(this.gameObject);
        }
    }
}
