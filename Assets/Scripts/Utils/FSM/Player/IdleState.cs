using Project.Data;
using UnityEngine;
using static Project.Data.GameData;

namespace Project.Systems.StateMachine
{
    public class IdleState : State
    {
        public IdleState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter();

            Character.Agent.isStopped = true;
            Character.Agent.speed = 0;
            Character.Animator.SetTrigger(GameData.PlayerIdleSword);

            Character.SetTarget(null);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.hasPath)
                FSM.ChangeState(Character.MovingState);
            else if (!Character.Agent.hasPath && Character.Agent.velocity.sqrMagnitude == 0)
                Character.Animator.SetTrigger(GameData.PlayerIdleSword);

            //if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.speed == Character.CurrentWalkSpeed)
            //    FSM.ChangeState(Character.StateWalk);
            //else if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.speed == Character.CurrentRunSpeed)
            //    FSM.ChangeState(Character.StateRun);
            //else if (Character.Agent.velocity.sqrMagnitude == 0 && Character.Agent.speed == 0)
            //    Character.Animator.SetTrigger(GameData.PlayerIdleSword);
        }
    }
}