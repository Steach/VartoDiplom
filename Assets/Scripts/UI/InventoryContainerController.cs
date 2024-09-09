using Project.Systems.ControlsSystem;
using Project.Systems.ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Project.Controllers.UI
{
    public class InventoryContainerController : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ItemDataBase _itemDataBase;
        [SerializeField] private GameObject[] _itemSlots;
        private PlayerInventory _playerInventory;
        private ControlsSystem _inputActions;

        private void Start()
        {
            _playerInventory = _uiManager.GameManager.PlayerManager.PlayerInventory;
            _uiManager.InputActions.UIController.MousePointer.performed += CheckMousePosition;
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            _uiManager.InputActions.UIController.MousePointer.performed -= CheckMousePosition;
        }

        private void CheckMousePosition(InputAction.CallbackContext context)
        {
            var mousePosition = context.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                Debug.Log($"{clickedObject.name}");
            }
        }

        public void CheckInventory()
        {
            AddItemToInventar();
        }

        private void AddItemToInventar()
        {
            var countItem = _playerInventory.Inventory.Count;

            for (int i = 0; i < countItem; i++)
            {
                if (_playerInventory.Inventory.TryGetValue(i, out int id))
                {
                    foreach (var item in _itemDataBase.Items)
                    {
                        var idInBase = item.ItemID;

                        if ((int)idInBase == id)
                        {
                            var imageComponent = _itemSlots[i].GetComponent<Image>();
                            imageComponent.sprite = item.Icon;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}