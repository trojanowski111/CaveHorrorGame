using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIDialogueState", menuName = "AI/Dialogue/New dialogue state", order = 1)]
public class AIDialogueState : AIState
{
    public override void OnStart(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        Debug.Log("dialogueState");
    }
    public override void OnUpdate(AIAgent agent)
    {
        if(!agent.GetNPCDialogue().InDialogue())
        {
            agent.aiStateMachine.ChangeState(GetNextState());
        }
    }
    public override void OnExit(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = false;
        Debug.Log("leavingDialogue");
    }
}
