namespace Project.Systems.StateMachine.Enemy
{
    public class DeathState : EnemyStates
    {
        public DeathState(FSMEnemy characters, StateMachine FSM) : base(characters, FSM)
        {
        }
    }
}