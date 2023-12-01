using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChaseState : AiState
{
    private float timerToLose;
    public AiStateId GetId()
    {
        return AiStateId.Chase;
    }
    public void Enter(AiAgent agent)
    {
        // agent.audioSource.PlayOneShot(agent.spotAudio);
        agent.animator.SetTrigger("Spotted"); // this will go into the spot animation, and then automatically into chasing animation
        // agent.StartCoroutine(agent.Pause(0.5f));
        // agent.spotAudio.Play();
        
        timerToLose = agent.config.lostSightDuration;
        agent.navMeshAgent.speed = agent.config.chaseMoveSpeed;
        // agent.sensor.distance = agent.config.chaseDistance;
        // agent.sensor.angle = agent.config.chaseAngle;
    }
    public void Update(AiAgent agent)
    {
        ChaseMovement(agent);
    }
    public void Exit(AiAgent agent)
    {
        
    }
    void ChaseMovement(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.player.transform.position;

        // if(!agent.sensor.objects.Contains(agent.player.gameObject))
        // {
        //     timerToLose -= Time.deltaTime;
        //     if(timerToLose <= 0)
        //     {
        //         agent.stateMachine.ChangeState(AiStateId.Search);
        //     }
        // }
        // else
        // {
        //     timerToLose = agent.config.lostSightDuration;
        // }
    }
}
