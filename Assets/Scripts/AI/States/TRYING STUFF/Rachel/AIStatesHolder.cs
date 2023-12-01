using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIStatesHolder", menuName = "AI/States Holder/New State Holder", order = 1)]
public class AIStatesHolder : ScriptableObject
{
    [SerializeField] private AIState initialState;
    [SerializeField] private AIState[] states;

    private AIState previousInitialState;

    public AIState[] GetStates()
    {
        return states;
    }
    public AIState GetInitialState()
    {
        return initialState;
    }
    private void OnValidate()
    {
        // Check if the initial state has changed in the Editor
        if (initialState != previousInitialState)
        {
            PopulateStates();

            // Update the previous initial state
            previousInitialState = initialState;
        }
    }
    private void PopulateStates()
    {
        states = new AIState[] {initialState};
        
        AIState currentState = initialState;

        if(!currentState)
        return;

        while(currentState.GetNextState() != null && !ContainsState(currentState.GetNextState()))
        {
            currentState = currentState.GetNextState();
            AddState(currentState);
        }
    }
    private bool ContainsState(AIState state)
    {
        foreach(AIState existingState in states)
        {
            if(existingState == state)
            {
                return true;
            }
        }
        return false;
    }
    private void AddState(AIState state)
    {
        System.Array.Resize(ref states, states.Length + 1);
        states[states.Length - 1] = state;
    }
}
