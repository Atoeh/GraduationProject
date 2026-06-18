using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneScripts : MonoBehaviour
{
    [Header ("- Storyboard -")]
    [SerializeField]
    GameObject[] storyBoard;
    private int storyBoardSize;
    private int currScreen;
    private int nextScreen;

    [Header("- Scene transition -")]
    public float transTime;
    [SerializeField]
    private bool startsDark;
    private bool screenIsDark = false;
    [SerializeField]
    private Image image;
    private Color color;

    [Header("- Audio -")]
    [SerializeField]
    AudioClip ambienceClip;
    [SerializeField]
    AudioClip musicClip;
    [SerializeField]
    AudioClip transitionEffectClip;

    void Start()
    {
        color = image.GetComponentInChildren<Image>().color;

        storyBoardSize = storyBoard.Length + 1;
        currScreen = 0;

        for (int i = 0; i < storyBoard.Length; i++)
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

    // ----------------------- (RE-) START CUTSCENE -------------------------

    public void StartCutScene()
    {
        //Debug.Log("StartCutscene");
        GameEvents.TimerPause();
        StartCoroutine(Next());
    }

    //Not used rn
    public void RestartCutScene()
    {
        StartCoroutine(Next());
    }

    // ----------------------- NEXT SCREEN -------------------------

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
        //Debug.Log( "Scene before adding = " + currScreen);
        storyBoard[currScreen].SetActive(true);
        currScreen = (currScreen + 1) % (storyBoardSize);
        //Debug.Log("Scene after adding = " + currScreen);


        //Fade to UI
        StartCoroutine(Transition(false));
        yield return new WaitForSeconds(transTime);
    }

    // ----------------------- PREVIOUS SCREEN -------------------------

    public IEnumerator Previous()
    {
        //Fade to black
        StartCoroutine (Transition(true));
        yield return new WaitForSeconds(transTime);

        //disable ui
        Debug.Log("Scene before adding = " + currScreen);
        storyBoard[currScreen - 1].SetActive(false);
        
        //load previous ui
        storyBoard[currScreen - 2].SetActive(true);
        currScreen = currScreen - 1;

        //Fade to ui
        StartCoroutine(Transition(false));
        yield return new WaitForSeconds(transTime);
    }

    public void PreviousScreen()
    {
        StartCoroutine(Previous());
    }

    // ----------------------- CLOSE SCREEN -------------------------

    public IEnumerator Close()
    {
        //Fade to black
        StartCoroutine(Transition(true));
        yield return new WaitForSeconds(transTime);

        //disable ui (all for good measure)
        for (int i = 0; i < storyBoard.Length; i++)
        {
            storyBoard[i].SetActive(false);
        }

        //Fade to gameplay
        StartCoroutine(Transition(false));
        yield return new WaitForSeconds(transTime);
    }

    public void CloseScreen()
    {
        StartCoroutine(Close());
    }

    // ----------------------- TRANSITION SCREEN -------------------------

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
