using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject interactUI;
    [SerializeField]
    private GameObject fightUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CleanScreen();
    }


    /// <summary>
    /// Removes all the UI, exploration state UI
    /// </summary>
    public void CleanScreen()
    { 
        interactUI.SetActive(false);
        fightUI.SetActive(false);
    }
}
