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

        if(toggleObject == false)
            toggleObject.SetActive(true);
        else toggleObject.SetActive(false);
    }  
}
