using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    private GameObject targetObject;

    public void Setup(GameObject spawner)
    {
        //Debug.Log("SetupFunction");
        targetObject = spawner;
    }

    public void CloseUI()
    {
        //Debug.Log("CloseUI Function");
        GameEvents.ToggleMoveUI();
        Destroy(gameObject);
    }

    public void DestroySpawner()
    {
        //Debug.Log("DestroyFunction");
        //targetObject.GetComponent<OnInteractScript>().DeInstantiate();
        Destroy(targetObject);
    }


}
