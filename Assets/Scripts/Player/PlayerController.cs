using Project.Systems.ControlsSystem;
using Project.Systems.StateMachine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

namespace Project.Controllers.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FSMPlayer _characterFSM;
        private ControlsSystem _controlsSystem;
        private Camera _camera;
        [SerializeField] private bool _isFollowPlayer = false;
        [SerializeField] private bool _isFightingInPlace = false;
        [SerializeField] private bool _isRunning = false;

        private void Awake()
        {
            _controlsSystem = new ControlsSystem();
            _camera = Camera.main;
        }

        private void Update()
        {
            TurnToMouse();
            FollowPlayer();
        }

        private void OnEnable()
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

        private void OnDisable()
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
            if (!_isFightingInPlace && !_isRunning && _characterFSM.Agent.velocity.sqrMagnitude <= 0)
            {
                var mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                Plane playerPlane = new Plane(Vector3.up, transform.position);
                float hitDist = 0.0f;

                if (playerPlane.Raycast(ray, out hitDist))
                {
                    Vector3 targetPoint = ray.GetPoint(hitDist);
                    Vector3 direction = targetPoint - transform.position;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    targetRotation.x = 0;
                    targetRotation.z = 0;

                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime + 10f);
                }
            }
        }

        private void StartFight(InputAction.CallbackContext context) => _isFightingInPlace = true;
        private void StopFight(InputAction.CallbackContext context) => _isFightingInPlace = false;

        private void StartRun(InputAction.CallbackContext context) => _isRunning = true;
        private void StopRun(InputAction.CallbackContext context) => _isRunning = false;

        private void StartFollowPlayer(InputAction.CallbackContext context) => _isFollowPlayer = true;
        private void StopFollowPlayer(InputAction.CallbackContext context) => _isFollowPlayer = false;

        private void OnMouseClick(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit) && !_isFightingInPlace && !_isRunning && !hit.collider.CompareTag("Enemy"))
            {
                _characterFSM.Agent.speed = _characterFSM.PlayerData.WalkSpeed;
                _characterFSM.FSM.ChangeState(_characterFSM.StateWalk);
                _characterFSM.Agent.SetDestination(hit.point);
            }
            else if (_isFightingInPlace && (!_isRunning || _isRunning))
            {
                _characterFSM.FSM.ChangeState(_characterFSM.StateAttackInPlace);
            }
            else if (Physics.Raycast(ray, out hit) && !_isFightingInPlace && _isRunning)
            {
                _characterFSM.Agent.speed = _characterFSM.PlayerData.RunSpeed;
                _characterFSM.FSM.ChangeState(_characterFSM.StateRun);
                _characterFSM.Agent.SetDestination(hit.point);
            }

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Enemy") && !_isFightingInPlace)
            {
                Debug.Log("FIND_ENEMY");
                _characterFSM.SetTarget(hit.collider.gameObject);
                _characterFSM.FSM.ChangeState(_characterFSM.StateRunToEnemyAndAttake);
                //_characterFSM.Agent.speed = _characterFSM.PlayerData.RunSpeed;
                //_characterFSM.Agent.SetDestination(hit.point);
                //StartCoroutine(AttakeEnemy(2));
            }
        }

        private void FollowPlayer()
        {
            if (_isFollowPlayer && !_isFightingInPlace)
            {
                Vector2 mousePostion = _controlsSystem.PlayerController.Position.ReadValue<Vector2>();
                Ray ray = Camera.main.ScreenPointToRay(mousePostion);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    _characterFSM.Agent.SetDestination(hit.point);
                }
            }
        }

        //private IEnumerator AttakeEnemy(float minDistance)
        //{
        //    while (_characterFSM.Agent.remainingDistance >= minDistance)
        //    {
        //        yield return null;
        //    }
        //    _characterFSM.Agent.ResetPath();
        //    _characterFSM.FSM.ChangeState(_characterFSM.StateAttackInPlace);
        //}
    }
}