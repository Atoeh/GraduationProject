using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private bool quotaIsTrue = false;
    [SerializeField]
    private int quota;
    public int lootAmmount = 0;
    private bool quotaHit = false;

    void Start()
    {
        if (quota > 0)
            quotaIsTrue = true;

        //Function to set quota in UI?
        //Check UI manager
    }

    public void GoToNextLevel()
    {
        //next scene met modulo (%) dat het terug warped naar de eerste scene uit de index...
        int currScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = (currScene + 1) % SceneManager.sceneCountInBuildSettings;

        if (quotaIsTrue == false)
            SceneManager.LoadScene(nextScene);
        else 
        {
            if (lootAmmount >= quota || quotaHit == true)
                SceneManager.LoadScene(nextScene);
            else
                Debug.Log("Quota has not been hit, back to the mines ye go");
        }
    }

    public void ToggleHitQuota()
    { 
        if(quotaHit == false)
        quotaHit = true;
        else quotaHit = false;
    }
}
