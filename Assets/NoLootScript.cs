using System.Linq.Expressions;
using UnityEngine.UI;
using UnityEngine;

public class NoLootScript : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private GameObject manager;
    private void OnEnable()
    {
        GameEvents.OnNoLoot += MakeInteractive;
        if (manager.GetComponent<InventoryManagementScriptV2>().lootAmmount <= 0)
        {
            Debug.Log("Loot = 0 , MakeInteractive");
            MakeInteractive();
        }
    }

    private void OnDisable()
    {
        GameEvents.OnNoLoot -= MakeInteractive;
    }
    public void MakeInteractive()
    { 

        button.interactable = true;
    }
}
