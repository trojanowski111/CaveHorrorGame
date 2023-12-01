using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackState : AiState
{
    private float attackCooldown;
    public AiStateId GetId()
    {
        return AiStateId.Attack;
    }
    public void Enter(AiAgent agent)
    {
        agent.animator.SetTrigger("Attack");

        attackCooldown = agent.config.attackCooldown;
        agent.navMeshAgent.speed = agent.config.attackMoveSpeed;
        agent.StartCoroutine(agent.Pause(agent.config.attackCooldown));
    }
    public void Update(AiAgent agent)
    {
        attackCooldown -= Time.deltaTime;
        
        if(attackCooldown <= 0)
        {
            agent.animator.SetTrigger("Chase");
        }
    }
    public void Exit(AiAgent agent)
    {
        
    }
}
