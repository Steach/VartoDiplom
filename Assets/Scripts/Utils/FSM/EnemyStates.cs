namespace Project.Systems.StateMachine.Enemy
{
    public abstract class EnemyStates
    {
        protected FSMEnemy Character;
        protected StateMachine FSM;

        protected EnemyStates(FSMEnemy characters, StateMachine FSM)
        {
            this.Character = characters;
            this.FSM = FSM;
        }

        public virtual void Enter(object data = null) { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit(object data = null) { }
    }
}