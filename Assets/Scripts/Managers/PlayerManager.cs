using Project.Controllers.Player;
using Project.Data;
using Project.Systems.LevelingSystem;
using Project.Systems.StateMachine;
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

        private PlayerLevelingSystem _playerLevelingSystem;
        private PlayerController _playerController;
        private FSMPlayer _playerFSM;

        public PlayerLevelingSystem PlayerLevelingSystem { get { return _playerLevelingSystem; } }
        public PlayerController PlayerController { get { return _playerController; } }

        private void Awake()
        {
            _playerController = new PlayerController();
            _playerLevelingSystem = new PlayerLevelingSystem();
            _playerFSM = new FSMPlayer();
            _playerLevelingSystem.Init();
            _playerController.Init(_playerFSM, _camera, _playerTransform);
            _playerFSM.Init(_agent, _animator, _playerData, _debug, _playerTransform);
        }

        private void OnEnable()
        {
            _playerLevelingSystem.OnEnableEvents();
            _playerController.OnEnableEvents();
            _playerFSM.OnEnableEvents();
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
        }
    }
}