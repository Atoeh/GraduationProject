using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrashScript : MonoBehaviour
{
    private Image image;
    private Color originalColor;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip trashSound;

    [SerializeField] private float duration = 0.05f;
    [SerializeField] private float strength = 6f;
    [SerializeField] private float frequency = 25f;

    private Vector2 originalPosition;

    private void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void Highlight()
    {
        image.color = new Color(
            originalColor.r + 0.3f,
            originalColor.g + 0.3f,
            originalColor.b + 0.3f,
            originalColor.a
        );
    }

    public void ResetHighlight()
    {
        image.color = originalColor;
    }

    public void DisposeItem(GameObject itemVisual)
    {
        Destroy(itemVisual);
        ResetHighlight();
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(trashSound);
    }

    public void Shake()
    {
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;
        RectTransform rect = GetComponent<RectTransform>();

        while (elapsed < duration)
        {
            float x = (Random.value * 2f - 1f) * strength;
            float y = (Random.value * 2f - 1f) * strength;
            rect.anchoredPosition = originalPosition + new Vector2(x, y);
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(1f / frequency);
        }

        rect.anchoredPosition = originalPosition;
    }
}
