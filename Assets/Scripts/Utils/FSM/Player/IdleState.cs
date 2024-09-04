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

            //Character.Animator.SetTrigger(GameData.PlayerMovingTrigger);
            //var normalizedSpeed = Mathf.InverseLerp(Character.CurrentWalkSpeed, Character.CurrentRunSpeed, Character.Agent.speed);
            //Character.Animator.SetFloat(GameData.PlayerSpeed, normalizedSpeed);

            Character.SetTarget(null);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            //Character.Animator.SetTrigger(GameData.PlayerMovingTrigger);
            //var normalizedSpeed = Mathf.InverseLerp(Character.CurrentWalkSpeed, Character.CurrentRunSpeed, Character.Agent.speed);
            //Character.Animator.SetFloat(GameData.PlayerSpeed, normalizedSpeed);

            if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.speed == Character.CurrentWalkSpeed)
                FSM.ChangeState(Character.StateWalk);
            else if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.speed == Character.CurrentRunSpeed)
                FSM.ChangeState(Character.StateRun);
            else if (Character.Agent.velocity.sqrMagnitude == 0 && Character.Agent.speed == 0)
                Character.Animator.SetTrigger(GameData.PlayerIdleSword);
        }
    }
}