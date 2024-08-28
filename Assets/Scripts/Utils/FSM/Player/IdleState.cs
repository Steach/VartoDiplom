using Project.Data;
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
            Character.Animator.SetTrigger(GameData.PlayerIdleSword);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.speed == Character.PlayerData.WalkSpeed)
                FSM.ChangeState(Character.StateWalk);
            
            if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.speed == Character.PlayerData.RunSpeed)
                FSM.ChangeState(Character.StateRun);
        }
    }
}