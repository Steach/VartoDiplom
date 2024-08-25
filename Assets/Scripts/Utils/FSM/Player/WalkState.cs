using Project.Data;

namespace Project.Systems.StateMachine
{
    public class WalkState : State
    {
        public WalkState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Character.Animator.SetTrigger(GameData.PlayerWalkFrontPlaceSword);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.velocity.sqrMagnitude <= 0)
                FSM.ChangeState(Character.StateIdle);
        }
    }
}