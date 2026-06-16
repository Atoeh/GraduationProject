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
    private int nextScreen;

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
        Debug.Log("storyBoardLength = " + storyBoard.Length);
        currScreen = 0;

        for (int i = 0; i < storyBoardSize; i++)
        {
            storyBoard[i].SetActive(false);   
        }
        
        if (startsDark == true)
        {
            screenIsDark = true;
            color.a = 1f;
            image.color = color;

            //Wordt nu door UI manager gedaan
            //StartCutScene();
        }
    }

    public void StartCutScene()
    {
        //Pause the game
        GameEvents.TimerPause();
        StartCoroutine(Next());
    }

    public void RestartCutScene()
    {
        StartCoroutine(Next());
    }

    public void NextScreen()
    {
        StartCoroutine(Next());
    }

    public IEnumerator Next()
    {
        //fade to black
        if (screenIsDark == false)
        {
            StartCoroutine(Transition(true));
            yield return new WaitForSeconds(transTime);
        } else
        {
            screenIsDark = false;
        }

        //disable old UI
        if (currScreen > 0)
            storyBoard[currScreen - 1].SetActive(false);

        //enable new UI
        storyBoard[currScreen].SetActive(true);
        currScreen = (currScreen + 1) % (storyBoardSize + 1);
        Debug.Log("currScreen = " + currScreen);

        //Fade to UI
        StartCoroutine(Transition(false));
        yield return new WaitForSeconds(transTime);
    }

    public void PreviousScreen()
    {
        StartCoroutine(Transition(true));
        currScreen = (currScreen--);
        StartCoroutine(Transition(false));
    }

    public void CloseScreen()
    {
        StartCoroutine(Transition(true));
        
        for (int i = 0; i < storyBoardSize; i++)
        {
            storyBoard[i].SetActive(false);
        }

        StartCoroutine(Transition(false));
        //Unpause the game
        GameEvents.TimerPause();
    }

    public IEnumerator Transition(bool fadeInIsTrue)
    {
        //Debug.Log("Transition performing");
        //Debug.Log("TransTime = " + transTime);

        float timer = 0f;

        while (timer < transTime)
        {
            timer += Time.deltaTime;

            if (fadeInIsTrue == false)
            {
                color.a = 1f - Mathf.Clamp01(timer / transTime);
                //Debug.Log("FadingOut");
            }
            else
            {
                color.a = Mathf.Clamp01(timer / transTime);
                //Debug.Log("FadingIn");
            }

            image.color = color;
            yield return null;
        }
    }
}
