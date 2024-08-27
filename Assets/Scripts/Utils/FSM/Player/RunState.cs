using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine
{
    public class RunState : State
    {
        public RunState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter();

            Character.Agent.isStopped = false;
            Character.Agent.speed = Character.PlayerData.RunSpeed;
            Character.Animator.SetTrigger(GameData.PlayerRunFrontPlaceSword);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.velocity.sqrMagnitude <= 0)
                FSM.ChangeState(Character.StateIdle);
            if (Character.Agent.speed <= Character.PlayerData.WalkSpeed && Character.Agent.velocity.sqrMagnitude > 0)
                FSM.ChangeState(Character.StateWalk);
        }
    }
}

