using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LootUiScriptV3 : MonoBehaviour
{
    [Header("Loot objects and variables")]

    [SerializeField]
    private GameObject quotaObj;
    [SerializeField]
    private GameObject currLootObj;
    [SerializeField]
    private GameObject worthObj;
    [SerializeField]
    private GameObject allLootObj;

    private TMP_Text quotaText;
    private TMP_Text currLootText;
    private TMP_Text worthText;
    private TMP_Text allLootText;

    [SerializeField]
    private float quotaValue;
    [SerializeField]
    private float totalLootValue;

    [Header("Loot Tracker and visuals")]

    [SerializeField]
    private List<float> worthList;
    [SerializeField]
    private List<GameObject> imageList;
    [SerializeField]
    private int lootCount;
    private GameObject lootImage;
    [SerializeField]
    private GameObject spawnLocation;
    [SerializeField]
    private GameObject nullImage;

    [Header("player title")]
    [SerializeField]
    private GameObject titleObj;
    private TMP_Text title;
    [SerializeField]
    private String[] titles;
    private int titleIndex = 0;

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
        GameEvents.OnDropLoot -= ShowPreviousLoot;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalLootValue = 0f;
        quotaText = quotaObj.GetComponent<TMP_Text>();
        allLootText = allLootObj.GetComponent<TMP_Text>();
        currLootText = currLootObj.GetComponent<TMP_Text>();
        worthText = worthObj.GetComponent<TMP_Text>();
        title = titleObj.GetComponent<TMP_Text>();
        titleIndex = - 1;
    }

    private void Update()
    {

    }

    //Set Quota text, this will have to change i guess
    public void SetQuotaUI(float value)
    {
        Debug.Log("Quota UI updated, quota = " + value);
        quotaValue = value;
        if (quotaText == null)
            quotaText = quotaObj.GetComponent<TMP_Text>();
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
        AddToTotal(value);
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
            SubtractFromTotal();
            UpdateLootShowCase();
            //Debug.Log("Low");
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

    private void AddToTotal(float value)
    {
        totalLootValue += value;
        allLootText.text = totalLootValue.ToString();
    }

    private void SubtractFromTotal()
    {
        //lootCount--;
        if (lootCount < 0)
            lootCount = 0;

        Debug.Log("lootCount" + lootCount);
        //if (worthList.Count <= lootCount)
            totalLootValue -= worthList[lootCount];
        allLootText.text = totalLootValue.ToString();
    }

    public void ChangeTitle()
    {
        titleIndex = (titleIndex + 1) % titles.Length;
        Debug.Log("titleIndex = " + titleIndex);
        //if bug perhaps not void start performed, then write text by hand and only perform if titleIndex >= 1
        title.text = titles[titleIndex];
    }
}
