using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneScripts : MonoBehaviour
{
    [Header ("Storyboard")]
    [SerializeField]
    GameObject[] storyBoard;
    private int storyBoardSize;
    private int currScreen;

    [Header("Scene transition")]
    public float transTime;
    [SerializeField]
    private bool startsDark;
    private bool screenIsDark = false;
    [SerializeField]
    private Image image;
    private Color color;

    void Start()
    {
        color = GetComponentInChildren<Image>().color;

        storyBoardSize = storyBoard.Length;
        currScreen = 0;

        for (int i = 0; i < storyBoardSize; i++)
        {
            storyBoard[i].SetActive(false);   
        }
        
        if (startsDark == true)
        {
            screenIsDark = true;
            color.a = 1f;
            image .color = color;

            //Wordt nu door UI manager gedaan
            //StartCutScene();
        }
    }

    public void StartCutScene()
    {
        Next();
    }

    public void RestartCutScene()
    {
        Next();
    }

    public void Next()
    {
        //fade to black
        if (screenIsDark == false)
        {
            StartCoroutine(Transition());
        }

        //load UI
        storyBoard[currScreen].SetActive(true);
        currScreen = (currScreen ++) % storyBoardSize;
        
        //Fade to UI
        StartCoroutine(Transition());
    }

    public void Previous()
    {
        StartCoroutine(Transition());
        //Previous element
        StartCoroutine(Transition());
    }

    public void Close()
    {
        StartCoroutine(Transition());
        
        for (int i = 0; i < storyBoardSize; i++)
        {
            storyBoard[i].SetActive(false);
        }

        StartCoroutine(Transition());
    }

    public IEnumerator Transition()
    {
        Debug.Log("Transition performing");

        float timer = 0f;

        while (timer < transTime)
        {
            timer += Time.deltaTime;

            if (screenIsDark == false)
                color.a = Mathf.Clamp01(timer / transTime);
            else
                color.a = color.a - Mathf.Clamp01(timer / transTime);

            image.color = color;
            yield return null;
        }
    }
}
