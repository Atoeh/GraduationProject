using UnityEngine;

public class BPIntroScript : MonoBehaviour
{
    [SerializeField]
    GameObject line1;
    [SerializeField]
    GameObject line2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line1.SetActive(true);
        line2.SetActive(false);
    }

    public void Next()
    {
        line1.SetActive(false);
        line2.SetActive(true);
    }
}
