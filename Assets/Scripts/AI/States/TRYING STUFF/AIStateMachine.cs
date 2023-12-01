using System;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private Dictionary<string, AIState> stateDictionary;
    private AIState currentState;
    private AIAgent aiAgent;
    public AIStateMachine(AIAgent newAgent)
    {
        aiAgent = newAgent;
        stateDictionary = new Dictionary<string, AIState>(); // Initialize the dictionary
    }
    public void RegisterState(AIState state)
    {
        AIState instancedState = state.Init();
        stateDictionary.Add(state.GetStateId(), instancedState);
    }
    public void OnUpdate()
    {
        currentState?.OnUpdate(aiAgent);
    }
    public void ChangeState(AIState requestState)
    {
        if(!stateDictionary.TryGetValue(requestState.GetStateId(), out AIState newState))
        {
            RegisterState(requestState);
        }

        stateDictionary.TryGetValue(requestState.GetStateId(), out AIState state);
        currentState?.OnExit(aiAgent);
        currentState = state;
        currentState?.OnStart(aiAgent);
    }
    public AIState GetCurrentState()
    {
        return currentState;
    }
}
