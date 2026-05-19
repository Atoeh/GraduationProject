using UnityEngine;

public class FacePlayerScript : MonoBehaviour
{
    void Update()
    {
        Transform playerTrans = FindFirstObjectByType<PlayerMovement>().gameObject.transform;
        transform.LookAt(playerTrans);
    }
}
