using Project.Data;
using UnityEngine;
using static Project.Data.GameData;

namespace Project.Systems.StateMachine.Player
{
    public class IdleState : PlayerStates
    {
        public IdleState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter();

            Character.Agent.isStopped = true;
            Character.Agent.speed = 0;
            Character.Animator.SetTrigger(GameData.PlayerIdle);

            Character.SetTarget(null);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.hasPath)
                FSM.ChangeState(Character.MovingState);
            else if (!Character.Agent.hasPath && Character.Agent.velocity.sqrMagnitude == 0)
                Character.Animator.SetTrigger(GameData.PlayerIdle);
        }
    }
}