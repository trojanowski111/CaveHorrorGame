using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AiHealth : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;
    AiAgent agent;

    [Header("General Variables")]
    public int currentHealth;
    public bool canBeDamaged;

    [Header("Drone Spawner")]
    public List<GameObject> drones;
    public GameObject barrier;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AiAgent>();
        currentHealth = agent.config.maxHealth;
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        // agent.takeDamageAudio.Play();
        if(drones.Count <= 0)
        {
            currentHealth -= damageAmount;
            if(agent.navMeshAgent != null && agent.navMeshAgent.enabled)
            {
                agent.navMeshAgent.destination = agent.player.position;
            }
            if(currentHealth <= 0)
            {
                Death();
            }
            if(OnHealthChanged != null)
            {
                OnHealthChanged(this, EventArgs.Empty);
            }
        }
    }
    public void Death()
    {
        if(OnDeath != null)
        {
            OnDeath(this, EventArgs.Empty);
        }
        agent.stateMachine.ChangeState(AiStateId.Death);
    }
    public void AddDroneToList(GameObject newObject)
    {
        drones.Add(newObject);
    }
}
