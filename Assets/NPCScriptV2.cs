using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCScriptV2 : MonoBehaviour
{
    [Header("- CutScenes -")]

    [SerializeField]
    private GameObject cutScene;
    [SerializeField]
    private int badEnding;
    [SerializeField]
    private int goodEnding;

    [Header ("- NPC Loot -")]

    [SerializeField]
    private GameObject[] lootCollection;
    [SerializeField]
    private int lootIndex;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.lightGreen;
        Gizmos.DrawSphere(transform.position, .5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("OpenDoor");
            //cutScene.GetComponent<CutSceneScripts>().StartCutScene();
            GameEvents.OpenDoor();
        }
    }
    public void StealLoot()
    {
        Debug.Log("Steal loot of NPC + " + lootIndex);
        lootCollection[lootIndex].GetComponent<LootPickUpV2>().PickUP();
        lootIndex = (lootIndex + 1) % lootCollection.Length;
    }

    public void DropLoot()
    {
        GameEvents.DropLoot();
    }

    public void BadEnding()
    {
        SceneManager.LoadScene(badEnding);
    }

    public void GoodEnding()
    {
        SceneManager.LoadScene(goodEnding);
    }
}
