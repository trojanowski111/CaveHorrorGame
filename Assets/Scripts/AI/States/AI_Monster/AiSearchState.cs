using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSearchState : AiState
{
    private Transform lastPlayerPos;
    private Vector3 searchPos;
    private float searchPosTimer;
    private float searchDuration;

    public AiStateId GetId()
    {
        return AiStateId.Search;
    }
    public void Enter(AiAgent agent)
    {
        lastPlayerPos = agent.player;    
        agent.StartCoroutine(agent.Pause(0.5f));    

        searchDuration = agent.config.searchingDuration;
        searchPosTimer = agent.config.searchingTimer;
        agent.navMeshAgent.speed = agent.config.searchingMoveSpeed;
        // agent.sensor.distance = agent.config.searchDistance;
        // agent.sensor.angle = agent.config.searchAngle;
        
        agent.animator.SetTrigger("Lost");
    }
    public void Update(AiAgent agent)
    {
        SearchingMovement(agent);
    }
    public void Exit(AiAgent agent)
    {
        
    }
    void SearchingMovement(AiAgent agent)
    {
        searchDuration -= Time.deltaTime;
        searchPosTimer -= Time.deltaTime;
        agent.navMeshAgent.destination = searchPos;

        if(searchDuration > 0)
        {
            if(searchPosTimer <= 0)
            {
                searchPos = new Vector3(
                Random.Range(lastPlayerPos.position.x - 10, lastPlayerPos.position.x + 10),
                lastPlayerPos.position.y,
                Random.Range(lastPlayerPos.position.z - 10, lastPlayerPos.position.z + 10));

                searchPosTimer = agent.config.searchingTimer;
            }
        }
        else if(searchDuration <= 0)
        {
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
    }
}
