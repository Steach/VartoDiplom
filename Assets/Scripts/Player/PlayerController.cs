using Project.Data;
using Project.Systems.ControlsSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Project.Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private GameData.PlayerData _playerData;
        [SerializeField] private Animator _animator;
        private ControlsSystem _controlsSystem;
        private Camera _camera;

        public Animator Animator { get { return _animator; } }

        private void Awake()
        {
             _controlsSystem = new ControlsSystem();
            _camera = Camera.main;
            _agent.speed = _playerData.WalkSpeed;
            _agent.angularSpeed = _playerData.RotateSpeed;
            _animator.SetTrigger(GameData.PlayerIdleSword);
        }

        private void Update()
        {
            if (_agent.velocity.sqrMagnitude == 0f)
                _animator.SetTrigger(GameData.PlayerIdleSword);
            else
                _animator.SetTrigger(GameData.PlayerWalkFrontPlaceSword);
        }

        private void OnEnable()
        {
            _controlsSystem.PlayerController.Enable();
            _controlsSystem.PlayerController.Movement.performed += OnMove;
        }

        private void OnDisable()
        {
            _controlsSystem.PlayerController.Disable();
            _controlsSystem.PlayerController.Movement.performed -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}