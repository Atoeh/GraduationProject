using Unity.VisualScripting;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject toggleObject;

    public void ToggleObject()
    {
        if (toggleObject == null)
            toggleObject = gameObject;

        if(toggleObject.activeSelf == false)
            toggleObject.SetActive(true);
        else toggleObject.SetActive(false);
    }  
}
