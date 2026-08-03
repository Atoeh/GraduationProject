using UnityEngine;
using System.Collections;

public class RumbleScript : MonoBehaviour
{
    [Header("Shake Settings")]
    public float duration = 0.3f;
    public float strength = 10f;
    public float frequency = 25f;
    public bool fadeOut = true;

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private Coroutine shakeRoutine;

    //see if i can make the appropriate game Object shake
    private bool inSpawn;
    private bool inGrid;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void Shake()
    {
        if (shakeRoutine != null)
            StopCoroutine(shakeRoutine);

        shakeRoutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float damper = fadeOut ? 1f - (elapsed / duration) : 1f;

            float x = (Random.value * 2f - 1f) * strength * damper;
            float y = (Random.value * 2f - 1f) * strength * damper;

            rectTransform.anchoredPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return new WaitForSeconds(1f / frequency);
        }

        rectTransform.anchoredPosition = originalPosition;
        shakeRoutine = null;
    }
}
