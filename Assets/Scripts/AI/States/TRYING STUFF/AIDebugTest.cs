using UnityEngine;

[CreateAssetMenu(fileName = "AIStates", menuName = "AI/Debug/Debug state", order = 1)]
public class AIDebugTest : AIState
{
    [SerializeField] private AIState nextState;
    [SerializeField] private float timer;
    private float countdown;

    public override void OnStart(AIAgent agent)
    {
        countdown = 0;
    }
    public override void OnUpdate(AIAgent agent)
    {
        Debug.Log(countdown);
        countdown += Time.deltaTime;

        if(countdown > timer)
        {
            Debug.Log(agent);
            agent.stateSCHandler.ChangeState(nextState);
        }
    }
    public override void OnExit(AIAgent agent)
    {

    }
}
