using UnityEngine;

public class ClosePopUpScript : MonoBehaviour
{
    public void CloseUI()
    {
        //Debug.Log("CloseUI Function");
        GameEvents.ToggleMoveUI();
        Destroy(gameObject);
    }
}
