using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [HideInInspector] public AiStateMachine stateMachine;
    
    [Header("General")]
    [HideInInspector] public Transform player;
    [HideInInspector] public Vector3 startingPos;

    [Header("General Components")]
    public Animator animator;
    public NavMeshAgent navMeshAgent;

    [Header("AI Components")]
    public AiAgentConfig config;
    public AiStateId initialState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        stateMachine = new AiStateMachine(this);
        RegisterStates();
        stateMachine.ChangeState(initialState);

        startingPos = transform.position;
    }

    void RegisterStates()
    {
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiChaseState());
        stateMachine.RegisterState(new AiAttackState());
        stateMachine.RegisterState(new AiSearchState());
    }
    void Update()
    {
        stateMachine.Update();
    }
    public IEnumerator Pause(float timer) // can be used for things like animations when player gets detected, like the goomba in mario where they will stop moving and play an animation and then chase
    {
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(timer); // for now it'll be zero but if it needs to be used just add timer inside - WaitForSeconds(timer)
        navMeshAgent.isStopped = false;
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
}
