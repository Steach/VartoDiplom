namespace Project.Systems.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Init(State startingState)
        {
            CurrentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState) 
        {
            CurrentState.Exit();

            CurrentState = newState;
            newState.Enter();
        }
    }
}