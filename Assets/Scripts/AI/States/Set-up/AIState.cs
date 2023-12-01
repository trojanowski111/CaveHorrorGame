using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum AiStateId
    {
        //dummy
        Idle,
        Chase,
        Attack,
        Search,
        Death,

        //shooter
        Shoot,
        IdleShoot,

        //idle static enemy
        Static,

        // robber boss
        RobberIdleStage,
        RobberOneStage,
        RobberTwoStage,
        RobberThreeStage,
    }
    public interface AiState
    {
        AiStateId GetId();
        void Enter(AiAgent agent);
        void Update(AiAgent agent);
        void Exit(AiAgent agent);
    }
