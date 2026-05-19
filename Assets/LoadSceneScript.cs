using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    [SerializeField]
    Scene currScene;

    void Start()
    {
        currScene = SceneManager.GetActiveScene();    
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(currScene.name);
    }

    public void LoadNextScene()
    {
        GameEvents.ExitLevel();
    }

    public void LoadStartScene()
    {
        GameEvents.LeaveGame();
    }
}
