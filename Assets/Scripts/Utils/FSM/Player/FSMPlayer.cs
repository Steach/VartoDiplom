using Project.Data;
using Project.Managers.Player;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Systems.StateMachine.Player
{
    public class FSMPlayer
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private GameData.PlayerData _playerData;
        private TextMeshProUGUI _debug;
        private Transform _playerTransform;
        private PlayerManager _playerManager;

        public NavMeshAgent Agent { get { return _agent; } }
        public Animator Animator { get { return _animator; } }
        public GameData.PlayerData PlayerData { get { return _playerData; } }
        public Transform PlayerTransform { get { return _playerTransform; } }
        public PlayerManager PlayerManager { get { return _playerManager; } }

        public StateMachine FSM { get; private set; }
        public IdleState StateIdle { get; private set; }
        public AttakeInPlaceState StateAttackInPlace { get; private set; }
        public RunToEnemyAndAttakeState StateRunToEnemyAndAttake { get; private set; }
        public MovingState MovingState { get; private set; }

        public void Init(NavMeshAgent agent, Animator animator, GameData.PlayerData playerData, TextMeshProUGUI textMesh, Transform playerTransform, PlayerManager playerManager)
        {
            _agent = agent;
            _animator = animator;
            _playerData = playerData;
            _debug = textMesh;
            _playerTransform = playerTransform;
            _playerManager = playerManager;

            FSM = new StateMachine();
            StateIdle = new IdleState(this, FSM);
            StateAttackInPlace = new AttakeInPlaceState(this, FSM);
            StateRunToEnemyAndAttake = new RunToEnemyAndAttakeState(this, FSM);
            MovingState = new MovingState(this, FSM);

            FSM.Init(StateIdle, _debug);

            _agent.speed = _playerManager.PlayerCharacteristicsData.CurrentWalkSpeed;
            _agent.angularSpeed = _playerData.RotateSpeed;
        }

        public void RunOnUpdate()
        {
            FSM.CurrentPlayerState.LogicUpdate();
        }

        public void RunOnFixedUpdate()
        {
            FSM.CurrentPlayerState.PhysicsUpdate();
        }

        public void OnEnableEvents()
        {
            EventBus.Subscribe<UpdateInventoryVisual>(SetPlayerWeaponToAnimator);
        }
        
        public void OnDisableEvents()
        {
            EventBus.Unsubscribe<UpdateInventoryVisual>(SetPlayerWeaponToAnimator);
        }

        public void SetTarget(GameObject newTarget) => _playerData.Target = newTarget;

        public void PlayerIsRunning(bool IsRunning) => _playerData.IsRunning = IsRunning;

        private void SetPlayerWeaponToAnimator(UpdateInventoryVisual updateInventoryVisual)
        {
            _animator.SetInteger(GameData.RightWeaponType, _playerManager.PlayerInventory.EquipedWeapon[GameData.RightHandIndex]);
            _animator.SetInteger(GameData.LeftWeaponType, _playerManager.PlayerInventory.EquipedWeapon[GameData.LeftHandIndex]);
        }
    }
}