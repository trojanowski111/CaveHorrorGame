using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class AIAgent : MonoBehaviour
{
    [SerializeField] private AIStatesHolder AIStatesHolder;
    [SerializeField] private NPCDialogue aiDialogue;
    public NavMeshAgent navMeshAgent;
    [HideInInspector] public Vector3 startingPos;
    public AIStateMachine aiStateMachine;
    private AIState currentState; // for testing purposes

    private void Awake()
    {
        aiStateMachine = new AIStateMachine(this); // create a new instance of the state machine and assign this ai agent to the constructor
        for(int i = 0; i < AIStatesHolder.GetStates().Length; i++)
        {
            aiStateMachine.RegisterState(AIStatesHolder.GetStates()[i]);
        }
        aiStateMachine.ChangeState(AIStatesHolder.GetInitialState());
    }
    private void Start()
    {
        startingPos = transform.position;
    }
    private void Update()
    {
        aiStateMachine.OnUpdate();
        currentState = aiStateMachine.GetCurrentState();
    }
    public void SwitchState(AIState newState)
    {
        aiStateMachine.ChangeState(newState);
    }
    public bool CanMoveToPosition(Vector3 randomPos)
    {
        NavMeshHit hit;
        
        if(NavMesh.SamplePosition(randomPos, out hit, 1, NavMesh.AllAreas))
        {
            randomPos = hit.position;
            return true;
        }
        randomPos = Vector3.zero;
        return false;
    }
    public NPCDialogue GetNPCDialogue()
    {
        return aiDialogue;
    }
}
