using TMPro;
using Project.Systems.StateMachine.Player;
using Project.Systems.StateMachine.Enemy;

namespace Project.Systems.StateMachine
{
    public class StateMachine
    {
        public PlayerStates CurrentPlayerState { get; private set; }
        public EnemyStates CurrentEnemyState { get; private set; }
        public TextMeshProUGUI Debug { get; private set; }

        public void Init(PlayerStates startingState, TextMeshProUGUI debug, object data = null)
        {
            CurrentPlayerState = startingState;
            Debug = debug;
            startingState.Enter(data);
            Debug.text = CurrentPlayerState.ToString();
        }

        public void Init(EnemyStates startingState, object data = null)
        {
            CurrentEnemyState = startingState;
            startingState.Enter(data);
        }

        public void ChangeState(PlayerStates newState, object data = null) 
        {
            CurrentPlayerState.Exit(data);

            CurrentPlayerState = newState;
            Debug.text = CurrentPlayerState.ToString();
            newState.Enter(data);
        }

        public void ChangeState(EnemyStates newState, object data = null)
        {
            CurrentEnemyState.Exit(data);

            CurrentEnemyState = newState;
            newState.Enter(data);
        }
    }
}