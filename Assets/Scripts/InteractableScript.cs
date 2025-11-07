using UnityEngine;

using UnityEngine.UIElements;


public class InteractableScript : MonoBehaviour
{
    [SerializeField]
    private GameObject uiPanel;
    [SerializeField]
    private bool isInteracting;
    [SerializeField]
    private string nameInteract;
    
    public string title;
    public string content;
    public bool button1;
    public bool button2;

    public void OnInteract()
    { 
        uiPanel.SetActive(true);    
    }
}
