using UnityEngine;

[CreateAssetMenu(fileName = "AIDebugTest", menuName = "AI/Debug/New Debug state", order = 1)]
public class AIDebugTest : AIState
{
    [SerializeField] private float timer;
    private float countdown;
    
    public override void OnStart(AIAgent agent)
    {
        Debug.Log(this);
        countdown = 0;
    }
    public override void OnUpdate(AIAgent agent)
    {
        countdown += Time.deltaTime;

        if(countdown > timer)
        {
            agent.aiStateMachine.ChangeState(GetNextState());
        }
    }
    public override void OnExit(AIAgent agent)
    {

    }
}
