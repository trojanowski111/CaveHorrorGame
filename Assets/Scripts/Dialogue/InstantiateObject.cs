using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    [SerializeField] private DialogueEventScriptableObject dialogueEvent;
    [SerializeField] private GameObject objectToSpawn;

    private void OnEnable()
    {
        dialogueEvent.OnDialogueRaise += Spawn;
    }
    private void OnDisable()
    {
        dialogueEvent.OnDialogueRaise -= Spawn;
    }
    private void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
