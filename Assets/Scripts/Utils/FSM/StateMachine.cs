using TMPro;

namespace Project.Systems.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }
        public TextMeshProUGUI Debug { get; private set; }

        public void Init(State startingState, TextMeshProUGUI debug, object data = null)
        {
            CurrentState = startingState;
            Debug = debug;
            startingState.Enter(data);
            Debug.text = CurrentState.ToString();
        }

        public void ChangeState(State newState, object data = null) 
        {
            CurrentState.Exit(data);

            CurrentState = newState;
            Debug.text = CurrentState.ToString();
            newState.Enter(data);
        }
    }
}