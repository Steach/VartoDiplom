using Project.Data;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Systems.StateMachine
{
    public class FSMPlayer
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private GameData.PlayerData _playerData;
        private TextMeshProUGUI _debug;
        private Transform _playerTransform;
        private float _currentWalkSpeed;
        private float _currentRunSpeed;

        public NavMeshAgent Agent { get { return _agent; } }
        public Animator Animator { get { return _animator; } }
        public GameData.PlayerData PlayerData { get { return _playerData; } }
        public Transform PlayerTransform { get { return _playerTransform; } }
        public float CurrentWalkSpeed { get { return _currentWalkSpeed; } }
        public float CurrentRunSpeed {  get { return _currentRunSpeed; } }

        public StateMachine FSM;
        public IdleState StateIdle;
        public WalkState StateWalk;
        public AttakeInPlaceState StateAttackInPlace;
        public RunState StateRun;
        public RunToEnemyAndAttakeState StateRunToEnemyAndAttake;

        public void Init(NavMeshAgent agent, Animator animator, GameData.PlayerData playerData, TextMeshProUGUI textMesh, Transform playerTransform)
        {
            _agent = agent;
            _animator = animator;
            _playerData = playerData;
            _debug = textMesh;
            _playerTransform = playerTransform;

            FSM = new StateMachine();
            StateIdle = new IdleState(this, FSM);
            StateWalk = new WalkState(this, FSM);
            StateAttackInPlace = new AttakeInPlaceState(this, FSM);
            StateRun = new RunState(this, FSM);
            StateRunToEnemyAndAttake = new RunToEnemyAndAttakeState(this, FSM);

            FSM.Init(StateIdle, _debug);

            _agent.speed = _currentWalkSpeed;
            _agent.angularSpeed = _playerData.RotateSpeed;
        }

        public void RunOnUpdate()
        {
            FSM.CurrentState.LogicUpdate();
        }

        public void RunOnFixedUpdate()
        {
            FSM.CurrentState.PhysicsUpdate();
        }

        public void OnEnableEvents()
        {
            EventBus.Subscribe<AddStatsEvent>(SetCharacteristicsFromStats);
        }

        public void OnDisableEvents()
        {
            EventBus.Unsubscribe<AddStatsEvent>(SetCharacteristicsFromStats);
        }

        public void SetTarget(GameObject newTarget) => _playerData.Target = newTarget;

        private void SetCharacteristicsFromStats(AddStatsEvent addStatsEvent)
        {
            _currentWalkSpeed = _playerData.BaseWalkSpeed + (addStatsEvent.NewAGL / 5);
            _currentRunSpeed = _playerData.BaseRunSpeed + (addStatsEvent.NewAGL / 5);
        }
    }
}