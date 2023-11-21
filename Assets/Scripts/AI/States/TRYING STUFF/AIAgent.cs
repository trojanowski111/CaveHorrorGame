using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class AIAgent : MonoBehaviour
{
    [SerializeField] private AIState startState;
    public NavMeshAgent navMeshAgent;
    [HideInInspector] public Vector3 startingPos;
    public StateSCHandler stateSCHandler;
    private Vector3 movePosition;

    private void Awake()
    {
        // initialState.Init();
        stateSCHandler = new StateSCHandler(this);
    }
    private void Start()
    {
        startingPos = transform.position;
        stateSCHandler.ChangeState(startState);
        // stateSCHandler.ChangeState(startState);
    }
    private void Update()
    {
        stateSCHandler.OnUpdate();

        // if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        // {
        //     movePosition = new Vector3(startingPos.x + Random.insideUnitSphere.x * 5, startingPos.y, startingPos.z + Random.insideUnitSphere.z * 5);

        //     if(CanMoveToPosition(movePosition))
        //     {
        //         navMeshAgent.destination = movePosition;
        //     }
        // }
        // navMeshAgent.destination = movePosition;
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
    // private void Update()
    // {
    //     // initialState.GetStateId().OnUpdate(this);
    // }
    IEnumerator StopOnWaypoint(float timer)
    {
        navMeshAgent.speed = 0;
        
        yield return new WaitForSeconds(timer);

        navMeshAgent.speed = 5;
    }
}
