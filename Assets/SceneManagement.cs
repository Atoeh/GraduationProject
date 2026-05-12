using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private string currentSceneName; 
    private string nextSceneName; //miss vervangen met getallen

    [SerializeField]
    private bool quotaIsTrue;
    [SerializeField]
    private bool quotaHit = false;

    void Start()
    {        
        if (currentSceneName.Contains("Loop"))
            quotaIsTrue = true;
    }

    public void GoToNextLevel()
    {
        if (quotaIsTrue == false)
            SceneManager.LoadScene(nextSceneName);
        else 
        {
            if (quotaHit == true)
                SceneManager.LoadScene(nextSceneName);
            else
                Debug.Log("Quota has not been hit, back to the mines you go");
        }
    }

    public void QuotaHit()
    {
        quotaHit = true;
    }
}
