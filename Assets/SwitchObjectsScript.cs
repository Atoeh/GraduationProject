using UnityEngine;

public class SwitchObjectsScript : MonoBehaviour
{
    [SerializeField]
    GameObject obj1;
    [SerializeField]
    GameObject obj2;
    [SerializeField]
    float updateTime;
    float lastTime;

    private void Start()
    {
        lastTime = Time.time;
        obj1.SetActive(true);
        obj2.SetActive(false);
    }
    void Update()
    {
        if ((Time.time - lastTime) > updateTime)
        {
            Switch();
            lastTime = Time.time;
        }
    }

    void Switch()
    {
        if (obj1.activeSelf == false)
        {
            obj1.SetActive(true);
            obj2.SetActive(false);
        }
        else
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
        }
    }
}
