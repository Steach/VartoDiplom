using Project.Controllers.Player;
using Project.Systems.LevelingSystem;
using Project.Systems.StateMachine;
using UnityEngine;

namespace Project.Managers.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [Header("Player controller settings")]
        [SerializeField] private FSMPlayer _characterFSM;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private bool _isFollowPlayer = false;
        [SerializeField] private bool _isFightingInPlace = false;
        [SerializeField] private bool _isRunning = false;

        [Space]
        [Header("Leveling system settings")]
        private PlayerLevelingSystem _playerLevelingSystem;
        private PlayerController _playerController;

        public PlayerLevelingSystem PlayerLevelingSystem { get { return _playerLevelingSystem; } }
        public PlayerController PlayerController { get { return _playerController; } }

        private void Awake()
        {
            _playerController = new PlayerController();
            _playerLevelingSystem = new PlayerLevelingSystem();
            _playerLevelingSystem.Init();
            _playerController.Init(_characterFSM, _camera, _playerTransform);
        }

        private void OnEnable()
        {
            _playerLevelingSystem.OnEnableEvents();
            _playerController.OnEnableEvents();
        }

        private void Update()
        {
            _playerLevelingSystem.RunInUpdate();
            _playerController.RunOnUpdate();
        }

        private void OnDisable()
        {
            _playerLevelingSystem.OnDisableEvents();
            _playerController.OnDisableEvents();
        }
    }
}