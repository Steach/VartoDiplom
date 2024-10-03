namespace Project.Systems.StateMachine.Enemy
{
    public class AttakeState : EnemyStates
    {
        public AttakeState(FSMEnemy characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);

            Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.AttakeController;
        }
    }
}
