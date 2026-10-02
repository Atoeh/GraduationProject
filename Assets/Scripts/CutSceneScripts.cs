using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneScripts : MonoBehaviour
{
    [Header("- Spawner -")]
    public LootPickUpV3 spawner;

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

    private void Awake()
    {
        color = image.GetComponentInChildren<Image>().color;

        //storyBoardSize = storyBoard.Length + 1;
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
            image.color = color;
        }
    }

    // ----------------------- GAME MANAGER ----------------------------------

    public void RestartLevel() => GameManager.Instance.RestartLevel();

    public void NextLevel() => GameManager.Instance.LoadNextLevel();

    public void StartDungeon() => GameManager.Instance.StartDungeoneering();

    // ----------------------- FUNCTION FOR BUTTONS STATE CUTSCENE -------------------------

    public void StartCutScene()
    {
        Debug.Log("StartCutscene");
        //GameEvents.TimerPause();
        StartCoroutine(Next());
    }

    public void RestartCutScene()
    {
        StartCoroutine(Next());
    }

    public void DestroyCutScene()
    { 
        Destroy(gameObject);
    }

    // ----------------------- BUTTONS - CHANGE SCREEN -------------------------

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
        currScreen = (currScreen + 1) % (storyBoardSize);

        //Fade to UI
        StartCoroutine(Transition(false));
        yield return new WaitForSeconds(transTime);
    }

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

    public void CloseScreen()
    {
        StartCoroutine(Close());
    }

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

    // ----------------------- FUNCTIONS FOR BUTTONS FOR SPAWNER ------------------------

    public void PickUpSpawner()
    {
        if (spawner != null)
            spawner.PickUpItem();
    }

    public void DestroySpawner()
    {
        if (spawner != null)
            spawner.DestroyItem();
    }

    // ----------------------- TRANSITION SCREEN -------------------------

    public IEnumerator Transition(bool fadeInIsTrue)
    {
        float timer = 0f;

        while (timer < transTime)
        {
            timer += Time.deltaTime;

            if (fadeInIsTrue)
            {
                color.a = Mathf.Clamp01(timer / transTime);
                //Debug.Log(" is fading in");
            }
            else
            {
                color.a = 1f - Mathf.Clamp01(timer / transTime);
                //Debug.Log("is fading out);
            }

            image.color = color;
            yield return null;
        }
    }

}