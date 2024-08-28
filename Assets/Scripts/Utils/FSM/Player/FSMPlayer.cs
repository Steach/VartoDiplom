using Project.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Systems.StateMachine
{
    public class FSMPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameData.PlayerData _playerData;

        public NavMeshAgent Agent { get { return _agent; } }
        public Animator Animator { get { return _animator; } }
        public GameData.PlayerData PlayerData { get { return _playerData; } }

        public StateMachine FSM;
        public IdleState StateIdle;
        public WalkState StateWalk;
        public AttakeInPlaceState StateAttackInPlace;
        public RunState StateRun;
        public RunToEnemyAndAttakeState StateRunToEnemyAndAttake;

        private void Awake()
        {
            FSM = new StateMachine();
            StateIdle = new IdleState(this, FSM);
            StateWalk = new WalkState(this, FSM);
            StateAttackInPlace = new AttakeInPlaceState(this, FSM);
            StateRun = new RunState(this, FSM);
            StateRunToEnemyAndAttake = new RunToEnemyAndAttakeState(this, FSM);

            FSM.Init(StateIdle);

            _agent.speed = _playerData.WalkSpeed;
            _agent.angularSpeed = _playerData.RotateSpeed;
        }

        public void SetTarget(GameObject newTarget)
        {
            _playerData.Target = newTarget;
        }

        private void Update()
        {
            FSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            FSM.CurrentState.PhysicsUpdate();
        }
    }
}