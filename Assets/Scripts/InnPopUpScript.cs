using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InnPopUpScript : MonoBehaviour
{
    public bool canEnter;
    [SerializeField]
    Button nextButton;

    private void Start()
    {
    }

    public void CanEnter()
    { 
    }

    public void EnterInn()
    {
        GameEvents.ExitLevel();
    }

    public void ClosePopUP()
    {
        Destroy(gameObject);
    }
}
