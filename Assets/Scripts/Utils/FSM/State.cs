namespace Project.Systems.StateMachine
{
    public abstract class State
    {
        protected FSMPlayer Character;
        protected StateMachine FSM;

        protected State(FSMPlayer characters, StateMachine FSM)
        { 
            this.Character = characters;
            this.FSM = FSM;
        }

        public virtual void Enter() { }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}