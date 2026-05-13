using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//Should call this game manager?

public class SceneManagement : MonoBehaviour
{
    private bool quotaIsTrue = false;
    [SerializeField]
    private float quota;
    public float lootAmmount = 0;
    private bool quotaHit = false;

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += HitQuota;
    }

    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= HitQuota;
    }

    void Start()
    {
        if (quota > 0)
            quotaIsTrue = true;

        GameEvents.QuotaSet(quota);
        //GameEvents.LootPickUp(lootAmmount);
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

    public void HitQuota()
    { 
        if(quotaHit == false)
        quotaHit = true;
        Debug.Log("quota has been hit");
    }
}
