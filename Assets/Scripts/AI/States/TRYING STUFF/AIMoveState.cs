using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AIStates", menuName = "AI/Move/Move state", order = 1)]
public class AIMoveState : AIState
{
    [SerializeField] private AIState nextState;
    public float moveSpeed;
    private Vector3 movePosition;
    public override void OnStart(AIAgent agent)
    {
        // agent.navMeshAgent.speed = agent.config.idleMoveSpeed;
        agent.navMeshAgent.speed = moveSpeed;

        movePosition = new Vector3(agent.startingPos.x + Random.insideUnitSphere.x * 5, agent.startingPos.y, agent.startingPos.z + Random.insideUnitSphere.z * 5);

        if(agent.CanMoveToPosition(movePosition))
        {
            agent.navMeshAgent.destination = movePosition;
        }
    }
    public override void OnUpdate(AIAgent agent)
    {
        IdleMovement(agent);
    }
    public override void OnExit(AIAgent agent)
    {
    }
    void IdleMovement(AIAgent agent)
    {
        if (!agent.navMeshAgent.pathPending && agent.navMeshAgent.remainingDistance < 0.5f)
        {
            agent.stateSCHandler.ChangeState(nextState);
            agent.StartCoroutine(StopOnWaypoint(Random.Range(1.5f,2.5f), agent));
            movePosition = new Vector3(agent.startingPos.x + Random.insideUnitSphere.x * 5, agent.startingPos.y, agent.startingPos.z + Random.insideUnitSphere.z * 5);

            if(agent.CanMoveToPosition(movePosition))
            {
                agent.navMeshAgent.destination = movePosition;
            }
        }
        agent.navMeshAgent.destination = movePosition;
    }
    IEnumerator StopOnWaypoint(float timer, AIAgent agent)
    {
        agent.navMeshAgent.speed = 0;
        
        yield return new WaitForSeconds(timer);

        agent.navMeshAgent.speed = moveSpeed;
    }
}
