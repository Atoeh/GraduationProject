using UnityEngine;

public class EndSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            GameEvents.ExitLevel();
    }
}
