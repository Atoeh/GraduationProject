using UnityEngine;

public class LootCanvasDestroyScript : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnLootPickUp += DestroyVisual;
        GameEvents.OnDropLoot += AlsoDestroyVisual;
    }

    private void OnDisable()
    {
        GameEvents.OnLootPickUp -= DestroyVisual;
        GameEvents.OnDropLoot -= AlsoDestroyVisual;

    }

    private void DestroyVisual(float value, GameObject image)
    { 
        Destroy(gameObject);
    }

    private void AlsoDestroyVisual()
    {
        Destroy(gameObject);
    }

}
