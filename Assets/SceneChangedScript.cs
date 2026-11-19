using UnityEngine;

public class SceneChangedScript : MonoBehaviour
{
    void Start()
    {
        GameEvents.NewSceneLoaded();
        Debug.Log("NewSceneLoaded");
    }
}
