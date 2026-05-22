using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LootUiScript : MonoBehaviour
{
    [SerializeField]
    private GameObject quotaObj;
    [SerializeField]
    private GameObject currLootObj;
    [SerializeField]
    private GameObject popUpObj;

    private TMP_Text quotaText;
    private TMP_Text currLootText;
    private TMP_Text popUpLootText;

    private bool popUpIsTrue;
    [SerializeField]
    private float startTime;
    private float popUpTime = 0.4f;

    [SerializeField] 
    private float quotaValue;
    [SerializeField]
    private float currLootValue;

    private void OnEnable()
    {
        //GameEvents.OnQuotaSet += SetQuotaUI;
        GameEvents.OnLootPickUp += LootPopUP;    
    }

    private void OnDisable()
    {
    //    GameEvents.OnQuotaSet -= SetQuotaUI;
        GameEvents.OnLootPickUp -= LootPopUP;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currLootValue = 0f;
        quotaText = quotaObj.GetComponent<TMP_Text>();
        currLootText = currLootObj.GetComponent<TMP_Text>();
        popUpLootText = popUpObj.GetComponent<TMP_Text>();
        popUpIsTrue = false;
    }

    private void Update()
    {
        if (popUpIsTrue == true)
        {
            popUpObj.SetActive(true);

            if (Time.time - startTime >= popUpTime)
                popUpIsTrue = false;
        }
        else
            popUpObj.SetActive(false);
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
        currLootText.text = value.ToString();
    }

    public void LootPopUP(float value)
    {
        string text;
        if (value >= 0)
        {
            text = "+ " + value.ToString();
        }
        else
            text = value.ToString();

        popUpLootText.text = text;
        startTime = Time.time;
        popUpIsTrue = true;
    }
}
