using UnityEngine;
using UnityEngine.UI;

public class ButtonConditionScript : MonoBehaviour
{
    [Header("Condition Settings")]
    public bool isLargerThanValue;
    public bool isSmallerThanValue;
    public float value;

    public Button button;

    void Start()
    {
        CompareQuota();
    }

    void Update()
    {
        CompareQuota();
    }

    private void CompareQuota()
    {
        if (isLargerThanValue)
        {
            if (GameManager.Instance.currentValue > value)
                button.interactable = true;
            else
                button.interactable = false;
        }

        if (isSmallerThanValue)
            if (GameManager.Instance.currentValue < value)
                button.interactable = true;
            else
                button.interactable = false;

        if (!isLargerThanValue && !isSmallerThanValue)
            Debug.Log("No buttoncondition has been assigned.");
    }
}
