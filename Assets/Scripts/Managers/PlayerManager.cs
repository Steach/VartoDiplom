using Project.Controllers.Player;
using Project.Data;
using Project.Systems.ItemSystem;
using Project.Systems.LevelingSystem;
using Project.Systems.StateMachine.Player;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Managers.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [Space]
        [Header("Player controller settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;

        [Space]
        [Header("Leveling system settings")]

        [Space]
        [Header("FSM Settings")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameData.PlayerData _playerData;
        [SerializeField] private TextMeshProUGUI _debug;

        [Space]
        [Header("Equipment")]
        [SerializeField] private GameObject _mainWeaponPlace;

        private PlayerLevelingSystem _playerLevelingSystem;
        private PlayerController _playerController;
        private FSMPlayer _playerFSM;
        private PlayerInventory _playerInventory;
        private PlayerIndicators _playerIndicators;

        

        private PlayerCharacteristics _playerCharacteristicsData;
        public PlayerCharacteristics PlayerCharacteristicsData { get { return _playerCharacteristicsData; } }

        public PlayerLevelingSystem PlayerLevelingSystem { get { return _playerLevelingSystem; } }
        public PlayerController PlayerController { get { return _playerController; } }
        public PlayerInventory PlayerInventory { get { return _playerInventory; } }
        public PlayerIndicators PlayerIndicators {  get { return _playerIndicators; } }

        private void Awake()
        {
            _playerController = new PlayerController();
            _playerLevelingSystem = new PlayerLevelingSystem();
            _playerFSM = new FSMPlayer();
            _playerInventory = new PlayerInventory();
            _playerIndicators = new PlayerIndicators();
            _playerLevelingSystem.Init();
            _playerController.Init(_playerFSM, _camera, _playerTransform, this);
            _playerFSM.Init(_agent, _animator, _playerData, _debug, _playerTransform, this);
            _playerInventory.Init(_gameManager.ItemDataBase);
            _playerIndicators.Init(this);
        }

        private void OnEnable()
        {
            _playerLevelingSystem.OnEnableEvents();
            _playerController.OnEnableEvents();
            _playerFSM.OnEnableEvents();
            _playerInventory.OnEnableEvents();
            _playerIndicators.RunOnEnable();
            EventBus.Subscribe<AddStatsEvent>(SetCharacteristicsFromStats);
        }

        private void Start()
        {
            _playerLevelingSystem.RunOnStart();
        }

        private void Update()
        {
            _playerLevelingSystem.RunInUpdate();
            _playerController.RunOnUpdate();
            _playerFSM.RunOnUpdate();
            _playerIndicators.RunOnUpdate();
        }

        private void FixedUpdate()
        {
            _playerFSM.RunOnFixedUpdate();
        }

        private void OnDisable()
        {
            _playerLevelingSystem.OnDisableEvents();
            _playerController.OnDisableEvents();
            _playerFSM.OnDisableEvents();
            _playerInventory.OnDisableEvents();
            _playerIndicators.RunOnDisable();
            EventBus.Unsubscribe<AddStatsEvent>(SetCharacteristicsFromStats);
        }


        private void SetCharacteristicsFromStats(AddStatsEvent addStatsEvent)
        {
            _playerCharacteristicsData.CurrentMaxMP = _playerData.BaseHp + (addStatsEvent.NewSTR / 5);
            _playerCharacteristicsData.CurrentMaxMP = _playerData.BaseMana + (addStatsEvent.NewINT / 5);
            _playerCharacteristicsData.CurrentMaxST = _playerData.BaseEndurance + (addStatsEvent.NewSTR + addStatsEvent.NewAGL) / 7;
            _playerCharacteristicsData.CurrentWalkSpeed = _playerData.BaseWalkSpeed + (addStatsEvent.NewAGL / 5);
            _playerCharacteristicsData.CurrentRunSpeed = _playerData.BaseRunSpeed + (addStatsEvent.NewAGL / 5);
        }

        [System.Serializable]
        public struct PlayerCharacteristics
        {
            public float CurrentMaxHP;
            public float CurrentMaxMP;
            public float CurrentMaxST;
            public float CurrentWalkSpeed;
            public float CurrentRunSpeed;
        }
    }
}