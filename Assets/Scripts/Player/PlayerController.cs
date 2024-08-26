using Project.Systems.ControlsSystem;
using Project.Systems.StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FSMPlayer _characterFSM;
        private ControlsSystem _controlsSystem;
        private Camera _camera;
        [SerializeField] private bool _isFighting = false;
        [SerializeField] private bool _isRunning = false;

        private void Awake()
        {
            _controlsSystem = new ControlsSystem();
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _controlsSystem.PlayerController.Enable();
            _controlsSystem.PlayerController.Movement.performed += OnMouseClick;
            _controlsSystem.PlayerController.Fight.started += StartFight;
            _controlsSystem.PlayerController.Fight.canceled += StopFight;
            _controlsSystem.PlayerController.Run.started += StartRun;
            _controlsSystem.PlayerController.Run.canceled += StopRun;
        }

        private void OnDisable()
        {
            _controlsSystem.PlayerController.Disable();
            _controlsSystem.PlayerController.Movement.performed -= OnMouseClick;
        }

        private void StartFight(InputAction.CallbackContext context) => _isFighting = true;
        private void StopFight(InputAction.CallbackContext context) => _isFighting = false;

        private void StartRun(InputAction.CallbackContext context) => _isRunning = true;
        private void StopRun(InputAction.CallbackContext context) => _isRunning = false;

        private void OnMouseClick(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
            if (Physics.Raycast(ray, out RaycastHit hit) && !_isFighting && !_isRunning)
            {
                _characterFSM.Agent.isStopped = false;
                _characterFSM.Agent.speed = _characterFSM.PlayerData.WalkSpeed;
                _characterFSM.Agent.SetDestination(hit.point);
            }

            if (Physics.Raycast(ray, out hit) && _isFighting && (!_isRunning || _isRunning))
            {
                _characterFSM.FSM.ChangeState(_characterFSM.StateAttack);
            }

            if (Physics.Raycast(ray, out hit) && !_isFighting && _isRunning)
            {
                _characterFSM.Agent.isStopped = false;
                _characterFSM.Agent.speed = _characterFSM.PlayerData.RunSpeed;
                _characterFSM.Agent.SetDestination(hit.point);
            }
        }
    }
}