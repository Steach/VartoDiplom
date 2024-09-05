using Project.Systems.ControlsSystem;
using Project.Systems.StateMachine;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Project.Controllers.Player
{
    public class PlayerController
    {
        private FSMPlayer _characterFSM;
        private ControlsSystem _controlsSystem;
        private Camera _camera;
        private Transform _targetTransform;
        private bool _isFollowPlayer = false;
        private bool _isFightingInPlace = false;
        public bool IsRunning { get; private set; }

        public ControlsSystem ControlSystem { get { return _controlsSystem; } }

        public void Init(FSMPlayer fsmPlayer, Camera camera, Transform targetTransform)
        {
            _controlsSystem = new ControlsSystem();
            _characterFSM = fsmPlayer;
            _camera = camera;
            _targetTransform = targetTransform;
        }

        public void OnEnableEvents()
        {
            _controlsSystem.PlayerController.Enable();
            _controlsSystem.PlayerController.Movement.performed += OnMouseClick;
            _controlsSystem.PlayerController.Movement.started += StartFollowPlayer;
            _controlsSystem.PlayerController.Movement.canceled += StopFollowPlayer;
            _controlsSystem.PlayerController.Fight.started += StartFight;
            _controlsSystem.PlayerController.Fight.canceled += StopFight;
            _controlsSystem.PlayerController.Run.started += StartRun;
            _controlsSystem.PlayerController.Run.canceled += StopRun;
        }

        public void RunOnUpdate()
        {
            TurnToMouse();
            FollowPlayer();
        }

        public void OnDisableEvents() 
        {
            _controlsSystem.PlayerController.Disable();
            _controlsSystem.PlayerController.Movement.performed -= OnMouseClick;
            _controlsSystem.PlayerController.Movement.started -= StartFollowPlayer;
            _controlsSystem.PlayerController.Movement.canceled -= StopFollowPlayer;
            _controlsSystem.PlayerController.Fight.started -= StartFight;
            _controlsSystem.PlayerController.Fight.canceled -= StopFight;
            _controlsSystem.PlayerController.Run.started -= StartRun;
            _controlsSystem.PlayerController.Run.canceled -= StopRun;
        }

        private void TurnToMouse()
        {
            if (!_isFightingInPlace && !IsRunning && _characterFSM.Agent.velocity.sqrMagnitude <= 0)
            {
                var mousePosition = Input.mousePosition;
                Ray ray = _camera.ScreenPointToRay(mousePosition);
                Plane playerPlane = new Plane(Vector3.up, _targetTransform.position);
                float hitDist = 0.0f;

                if (playerPlane.Raycast(ray, out hitDist))
                {
                    Vector3 targetPoint = ray.GetPoint(hitDist);
                    Vector3 direction = targetPoint - _targetTransform.position;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    targetRotation.x = 0;
                    targetRotation.z = 0;

                    _targetTransform.rotation = Quaternion.Slerp(_targetTransform.rotation, targetRotation, Time.deltaTime + 10f);
                }
            }
        }

        private void StartFight(InputAction.CallbackContext context) => _isFightingInPlace = true;
        private void StopFight(InputAction.CallbackContext context) => _isFightingInPlace = false;

        private void StartRun(InputAction.CallbackContext context) => _characterFSM.PlayerIsRunning(true); //IsRunning = true;
        private void StopRun(InputAction.CallbackContext context) => _characterFSM.PlayerIsRunning(false); //IsRunning = false;

        private void StartFollowPlayer(InputAction.CallbackContext context) => _isFollowPlayer = true;
        private void StopFollowPlayer(InputAction.CallbackContext context) => _isFollowPlayer = false;

        private void OnMouseClick(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit) && !_isFightingInPlace && !hit.collider.CompareTag("Enemy"))
            {
                _characterFSM.FSM.ChangeState(_characterFSM.MovingState);
                _characterFSM.Agent.SetDestination(hit.point);
            }
            else if (_isFightingInPlace && (!_characterFSM.PlayerData.IsRunning || _characterFSM.PlayerData.IsRunning))
            {
                _characterFSM.FSM.ChangeState(_characterFSM.StateAttackInPlace);
            }

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy") && !_isFightingInPlace)
            {
                _characterFSM.SetTarget(hit.collider.gameObject);
                _characterFSM.FSM.ChangeState(_characterFSM.StateRunToEnemyAndAttake);
            }
        }

        private void FollowPlayer()
        {
            if (_isFollowPlayer && !_isFightingInPlace)
            {
                Vector2 mousePostion = _controlsSystem.PlayerController.Position.ReadValue<Vector2>();
                Ray ray = _camera.ScreenPointToRay(mousePostion);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    _characterFSM.Agent.SetDestination(hit.point);
                }
            }
        }
    }
}