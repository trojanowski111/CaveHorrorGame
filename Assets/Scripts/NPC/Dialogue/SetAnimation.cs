using UnityEngine;

public class SetAnimation : MonoBehaviour
{
    [SerializeField] private DialogueEventScriptableObject dialogueEventScriptableObject;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        dialogueEventScriptableObject.OnDialogueRaise += Animation;
    }
    private void OnDisable()
    {
        dialogueEventScriptableObject.OnDialogueRaise -= Animation;
    }
    private void Animation()
    {
        animator.SetTrigger("isRunning");
    }
}
