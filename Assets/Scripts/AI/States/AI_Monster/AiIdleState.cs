using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    private Vector3 movePosition;
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = agent.config.idleMoveSpeed;
        // agent.sensor.distance = agent.config.idleDistance;
        // agent.sensor.angle = agent.config.idleAngle;
        agent.animator.SetTrigger("Walking");

    }
    public void Update(AiAgent agent)
    {
        IdleMovement(agent);
    }
    public void Exit(AiAgent agent)
    {
        
    }
    void IdleMovement(AiAgent agent)
    {
        if (!agent.navMeshAgent.pathPending && agent.navMeshAgent.remainingDistance < 0.5f)
        {
            agent.StartCoroutine(StopOnWaypoint(Random.Range(1.5f,2.5f), agent));
            movePosition = new Vector3(agent.startingPos.x + Random.insideUnitSphere.x * agent.config.movementRadius, agent.startingPos.y, agent.startingPos.z + Random.insideUnitSphere.z * agent.config.movementRadius);

            if(agent.CanMoveToPosition(movePosition))
            {
                agent.navMeshAgent.destination = movePosition;
            }
        }
        // if(agent.sensor.objects.Contains(agent.player.gameObject))
        // {
        //     agent.EnemyIdleTypes(agent);
        // }
        agent.navMeshAgent.destination = movePosition;
    }
    IEnumerator StopOnWaypoint(float timer, AiAgent agent)
    {
        agent.navMeshAgent.speed = 0;
        agent.animator.SetTrigger("Idle");
        
        yield return new WaitForSeconds(timer);

        agent.navMeshAgent.speed = agent.config.idleMoveSpeed;
        agent.animator.SetTrigger("Walking");

    }
}
