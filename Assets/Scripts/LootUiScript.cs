using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootUiScript : MonoBehaviour
{
    [SerializeField]
    private GameObject quotaObj;
    [SerializeField]
    private GameObject currLootObj;

    private TMP_Text quotaText;
    private TMP_Text currLootText;
    
    [SerializeField] 
    private float quotaValue;
    [SerializeField]
    private float currLootValue;

    private void OnEnable()
    {
        //GameEvents.OnQuotaSet += SetQuotaUI;
        //GameEvents.OnLootPickUp += UpdateUI;    
    }

    private void OnDisable()
    {
    //    GameEvents.OnQuotaSet -= SetQuotaUI;
    //    GameEvents.OnLootPickUp -= UpdateUI;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currLootValue = 0f;
        quotaText = quotaObj.GetComponent<TMP_Text>();
        currLootText = currLootObj.GetComponent<TMP_Text>();
    }

    //Set Quota text
    public void SetQuotaUI(float value)
    {
        quotaValue = value;
        quotaText.text = quotaValue.ToString();
    }

    //Update the curr amm text 
    public void UpdateUI(float value)
    {
        //eigenlijk zou deze niet de value moeten berekenen???
        //InventoryManagement Item op speler maken
        //LootCounterOnderdeel van deze manager maken
        //berekend nieuwe lootwaarde en INventoryUIUpdateEvent triggered
        currLootText.text = value.ToString();
    }
}
