namespace Project.Systems.StateMachine.Enemy
{
    public class DeathState : EnemyStates
    {
        public DeathState(FSMEnemy characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);

            Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.DeathController;
        }
    }
}