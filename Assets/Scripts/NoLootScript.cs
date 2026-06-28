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
        bool isStart = true;
        GameEvents.OnNoLoot += MakeInteractive;
        if (manager.GetComponent<InventoryManagementScriptV2>().lootAmmount <= 0 && isStart == false)
        {
            Debug.Log("Loot = 0 , MakeInteractive");
            MakeInteractive();
        }
        else 
            isStart = false;
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
