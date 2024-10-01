namespace Project.Systems.StateMachine.Player
{
    public abstract class PlayerStates
    {
        protected FSMPlayer Character;
        protected StateMachine FSM;

        protected PlayerStates(FSMPlayer characters, StateMachine FSM)
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