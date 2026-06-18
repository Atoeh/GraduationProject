using System.Collections;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField]
    GameObject playerSpawn;
    Transform spawnTransform;

    private void OnEnable()
    {
        GameEvents.OnQuotaHit += MovePlayer;
    }

    private void OnDisable()
    {
        GameEvents.OnQuotaHit -= MovePlayer;
    }

    private void MovePlayer()
    {
        GameEvents.ToggleMoveUI();

        if (spawnTransform == null)
            spawnTransform = playerSpawn.transform;

        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;

        GameEvents.ToggleMoveUI();
    }

}
