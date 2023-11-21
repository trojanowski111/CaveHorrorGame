using System;
using System.Collections.Generic;
using UnityEngine;

public class StateSCHandler
{
    public AIState[] aiStates;
    private AIState currentState;
    private AIAgent aiAgent;
    public StateSCHandler(AIAgent newAgent)
    {
        aiAgent = newAgent;
    }
    public void OnUpdate()
    {
        currentState?.OnUpdate(aiAgent);
    }
    public void ChangeState(AIState newState)
    {
        currentState = newState.Init();
        currentState?.OnExit(aiAgent);
        currentState = newState;
        currentState?.OnStart(aiAgent);
    }
}
