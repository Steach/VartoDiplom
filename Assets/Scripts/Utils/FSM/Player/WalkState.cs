using Project.Data;

namespace Project.Systems.StateMachine
{
    public class WalkState : State
    {
        public WalkState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter();
            Character.Agent.isStopped = false;
            Character.Agent.speed = Character.PlayerData.WalkSpeed;
            Character.Animator.SetTrigger(GameData.PlayerWalkFrontPlaceSword);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

           if (Character.Agent.velocity.sqrMagnitude <= 0)
               FSM.ChangeState(Character.StateIdle);
           if (Character.Agent.speed > Character.PlayerData.WalkSpeed && Character.Agent.velocity.sqrMagnitude > 0)
               FSM.ChangeState(Character.StateRun);
        }

        public override void Exit(object data = null) 
        {
            base.Exit();
        }
    }
}