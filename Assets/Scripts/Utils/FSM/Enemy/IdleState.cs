namespace Project.Systems.StateMachine.Enemy
{
    public class IdleState : EnemyStates
    {
        public IdleState(FSMEnemy characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);

            Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.IdleController;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.velocity.sqrMagnitude > 0 && Character.Agent.hasPath)
            {
                Character.FSM.ChangeState(Character.MovingState);
            }

            if (Character.EnemySimple.Target != null)
            {
                Character.FSM.ChangeState(Character.AttackState);
            }
        }
    }
}

