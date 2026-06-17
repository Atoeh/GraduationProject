using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LootUiScriptV2 : MonoBehaviour
{
    [Header ("Loot objects and variables")]

    [SerializeField]
    private GameObject quotaObj;
    [SerializeField]
    private GameObject currLootObj;
    [SerializeField]
    private GameObject worthObj;

    private TMP_Text quotaText;
    private TMP_Text currLootText;
    private TMP_Text worthText;

    [SerializeField]
    private float quotaValue;
    [SerializeField]
    private float currLootValue;

    //[Header("Popup object and variables")]

    //[SerializeField]
    //private GameObject popUpObj;
    //private TMP_Text popUpLootText;
    //private bool popUpIsTrue;
    //private float startTime;
    //[SerializeField]
    //private float popUpTime = 0.4f;

    [Header("Loot Tracker and visuals")]

    [SerializeField]
    private List<float> worthList;
    [SerializeField]
    private List<GameObject> imageList;
    private int lootCount;
    private GameObject lootImage;
    [SerializeField]
    private GameObject spawnLocation;
    [SerializeField]
    private GameObject nullImage;

    private void OnEnable()
    {
        GameEvents.OnLootPickUp += LootValuePopUp;
        GameEvents.OnLootPickUp += ShowNewloot;
        GameEvents.OnDropLoot += ShowPreviousLoot;
    }

    private void OnDisable()
    {
        GameEvents.OnLootPickUp -= LootValuePopUp;
        GameEvents.OnLootPickUp -= ShowNewloot;
        GameEvents.OnDropLoot += ShowPreviousLoot;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currLootValue = 0f;
        quotaText = quotaObj.GetComponent<TMP_Text>();
        currLootText = currLootObj.GetComponent<TMP_Text>();
        //popUpLootText = popUpObj.GetComponent<TMP_Text>();
        worthText = worthObj.GetComponent<TMP_Text>();
        //popUpIsTrue = false;
    }

    private void Update()
    {
        //if (popUpIsTrue == true)
        //{
        //    popUpObj.SetActive(true);

        //    if (Time.time - startTime >= popUpTime)
        //        popUpIsTrue = false;
        //}
        //else
        //    popUpObj.SetActive(false);
    }

    //Set Quota text, this will have to change i guess
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

    public void LootValuePopUp(float value, GameObject image)
    {
        //string text;
        //text = value.ToString();
        //popUpLootText.text = text;
        //worthText.text = text;
        //startTime = Time.time;
        //popUpIsTrue = true;
    }

    public void ShowNewloot(float value, GameObject image)
    {
        worthList.Add(value);
        imageList.Add(image);
        UpdateLootShowCase();
        lootCount++;
    }

    public void ShowPreviousLoot()
    {
        if (lootCount >= 1)
        {
            lootCount--;
            UpdateLootShowCase();
            Debug.Log("Low");
        }
        else
        {
            Instantiate(nullImage, spawnLocation.transform);
            worthText.text = "0";
            GameEvents.NoLoot();
        }
    }

    private void UpdateLootShowCase()
    {
        GameObject image = imageList[lootCount];
        Instantiate(image, spawnLocation.transform);

        worthText.text = worthList[lootCount].ToString();
    }
}
