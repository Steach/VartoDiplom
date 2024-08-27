namespace Project.Systems.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Init(State startingState, object data = null)
        {
            CurrentState = startingState;
            startingState.Enter(data);
        }

        public void ChangeState(State newState, object data = null) 
        {
            CurrentState.Exit(data);

            CurrentState = newState;
            newState.Enter(data);
        }
    }
}