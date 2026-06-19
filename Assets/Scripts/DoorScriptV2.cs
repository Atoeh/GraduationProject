using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DoorScriptV2 : MonoBehaviour
{
    [SerializeField]
    private GameObject popUpPrefab1;
    [SerializeField]
    private GameObject popUpPrefab2;

    [SerializeField]
    private GameObject popUpLocation;
    private GameObject popUpChoice;
    GameObject popUp;
    [SerializeField]
    private bool canEnter;

    private void Start()
    {
        popUpChoice = popUpPrefab1;
    }

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += PickUP;
    }

    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= PickUP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            popUp = Instantiate(popUpChoice, popUpLocation.transform);
        }
    }

    public void PickUP()
    {
        popUpChoice = popUpPrefab2;
    }
}
