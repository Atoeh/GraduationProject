using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//Should call this game management?
//1 maak apparte inventory manager om loot te tracken
//Laat game manager alleen maar luisteren naar events en dan andere scripts aan slaan

public class SceneManagement : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnExitLevel += GoToNextLevel;
        GameEvents.OnCandleDepleted += GameOver;
        //Niet op reachen van de deur maar op klikken van next level na merchant
        //GameEvents.OnOpenDoor += GoToNextLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnExitLevel += GoToNextLevel;
        GameEvents.OnCandleDepleted -= GameOver;
        //Niet op reachen van de deur maar op klikken van next level na merchant
        //GameEvents.OnOpenDoor -= GoToNextLevel;
    }

    public void GoToNextLevel()
    {
        //next scene met modulo (%) dat het terug warped naar de eerste scene uit de index...
        int currScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = (currScene + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextScene);
    }

    public void GameOver()
    { 
        //Trigger the game over state, which is an event i suppose
        //Why use an event to trigger a function to trigger an event?
        //Well, there can be more events that trigger the game over state.
    }
}
