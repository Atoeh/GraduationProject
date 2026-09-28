using UnityEngine;

public class GameManagerBridge : MonoBehaviour
{
    //public void SwitchGameState()
    //{
    //    //GameManager.Instance.SwitchGameState();
    //}

    public void RestartLevel() => GameManager.Instance.RestartLevel();

    public void NextLevel() => GameManager.Instance.LoadNextLevel();

    public void StartDungeon() => GameManager.Instance.StartDungeoneering();
}
