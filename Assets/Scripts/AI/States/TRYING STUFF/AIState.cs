using System;
using UnityEngine;

public abstract class AIState : ScriptableObject
{
    [SerializeField] private AIState nextState;
    private string stateID;
    public AIState GetNextState() => nextState;
    public string GetStateId() => stateID;
    // public string GetStateId() => this.GetType().ToString(); // this was what I had in the beginning, doesn't work with 2 same scriptable objects in the dictionary
    public AIState Init()
    {
        var state = ScriptableObject.Instantiate(this);
        stateID = state.name;
        return state;
    }
    public abstract void OnStart(AIAgent agent);
    public abstract void OnUpdate(AIAgent agent);
    public abstract void OnExit(AIAgent agent);
}
