using Unity.VisualScripting;
using UnityEngine;

public class SceneManagerV2 : MonoBehaviour
{
    [SerializeField] private float quota;
    [SerializeField] private GameObject inventorySystem;

    private float currentValue;
    private float oldValue;

    private void Update()
    {
        if (currentValue > oldValue)
        { 
            
        }
    }
}
