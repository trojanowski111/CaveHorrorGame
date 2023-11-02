using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private NPCHeadLook npcHeadLook;
    [SerializeField] private Canvas talkPrompt;
    [SerializeField] private Transform focusPointForPlayer;

    public void PlayerClose(Transform playerPos)
    {
        npcHeadLook.LookAt(playerPos);
        talkPrompt.enabled = true;

        talkPrompt.transform.LookAt(playerPos);
    }
    public void PlayerLeft()
    {
        talkPrompt.enabled = false;
    }
    public void DialogueStarted()
    {
        talkPrompt.enabled = false;
        Debug.Log("TALKING");
    }
    public Transform GetFocusPoint()
    {
        return focusPointForPlayer;
    }
}
