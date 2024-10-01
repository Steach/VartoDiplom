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
    }
}

