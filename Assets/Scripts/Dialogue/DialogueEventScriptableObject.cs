using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/DialogueEvent", order = 1)]
public class DialogueEventScriptableObject : ScriptableObject
{
    public delegate void DialogueAction();
    public event DialogueAction OnDialogueRaise;
    public void RaiseEvent()
    {
        OnDialogueRaise?.Invoke();
    }
}
