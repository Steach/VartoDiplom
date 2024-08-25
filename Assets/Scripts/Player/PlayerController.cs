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

        private void Awake()
        {
            _controlsSystem = new ControlsSystem();
            _camera = Camera.main;
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
                _characterFSM.Agent.SetDestination(hit.point);
            }
        }
    }
}