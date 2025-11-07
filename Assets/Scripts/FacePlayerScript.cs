using UnityEngine;

public class FacePlayerScript : MonoBehaviour
{
    [SerializeField]
    private Transform playerTrans;

    void Update()
    {
        transform.LookAt(playerTrans);
    }
}
