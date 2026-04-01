using UnityEngine;

public class DestroyUiSpawnerScript : MonoBehaviour
{
    private GameObject targetObject;

    public void Setup(GameObject spawner)
    {
        //Debug.Log("SetupFunction");
        targetObject = spawner;
    }

    public void DestroySpawner()
    {
        //Debug.Log("DestroyFunction");
        Destroy(targetObject);
    }

}
