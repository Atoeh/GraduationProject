using JetBrains.Annotations;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScriptVEnd : MonoBehaviour
{
    [Header("- CutScenes and Panels -")]

    [SerializeField]
    private GameObject introPanel;
    [SerializeField]
    private float transitionTime;

    void Start()
    {
        if (introPanel != null)
        {
            introPanel.GetComponent<CutSceneScripts>().transTime = transitionTime;
            IntroScreen();
        }
    }

    void IntroScreen()
    {
        introPanel.GetComponent<CutSceneScripts>().StartCutScene();
    }
}