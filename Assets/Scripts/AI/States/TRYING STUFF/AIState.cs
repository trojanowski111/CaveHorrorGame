using System;
using UnityEngine;

public class AIState : ScriptableObject
{
    [HideInInspector] public AIState state;
    public AIState Init()
    {
        state = ScriptableObject.Instantiate(this);
        return state;
    }
    // public Guid GetStateId()
    // {
    //     return stateID;
    // }
    public virtual void OnStart(AIAgent agent)
    {

    }
    public virtual void OnUpdate(AIAgent agent)
    {

    }
    public virtual void OnExit(AIAgent agent)
    {
        Destroy(this);
    }
}
