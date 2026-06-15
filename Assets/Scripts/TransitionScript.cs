using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    private float transTime;
    private bool transFromBlack = false;
    [SerializeField]
    private Image image;
    private Color color;

    void Start()
    {
        color = GetComponentInChildren<Image>().color;
    }

    public void TransitionCalled(float transitionTime)
    {
        Debug.Log("TransitionCalled");
        transTime = transitionTime;

        if (transFromBlack == true)
        {
            transFromBlack = false;
        }
        else
        {
            transFromBlack = true;
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

            if (transFromBlack == false)
                color.a = Mathf.Clamp01(timer / transTime);
            else
                color.a = color.a - Mathf.Clamp01(timer / transTime);

            image.color = color;
            yield return null;
        }
    }
}
