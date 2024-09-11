using Project.Systems.ControlsSystem;
using Project.Systems.ItemSystem;
using System.Collections.Generic;
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
        [SerializeField] private GameObject _inventoryEmptySlotContainer;
        [SerializeField] private GameObject _inventoryItemSlotContainer;
        [SerializeField] private List<GameObject> _itemSlots = new List<GameObject>();

        [SerializeField] private GameObject _emptySlotPrefab;
        [SerializeField] private GameObject _itemSlotPrefab;

        [SerializeField] private GameObject _itemLinesPrefab;
        [SerializeField] private GameObject _emptyLinesPrefab;

        [SerializeField] private int _countsOfSlotsInLine;
        [SerializeField] private int _countsOfLines;

        [SerializeField] private GameObject _itemInformationPopup;

        private PlayerInventory _playerInventory;
        private ControlsSystem _inputActions;

        public ItemDataBase ItemDataBase { get { return _itemDataBase; } }


        private void Awake()
        {
            GenerateInventorySlots();
        }

        private void Start()
        {
            _playerInventory = _uiManager.GameManager.PlayerManager.PlayerInventory;
            _uiManager.InputActions.UIController.MousePointer.performed += CheckMousePosition;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<ClickInItemSlotEvent>(AddInfoInPopup);
        }

        private void OnDisable()
        {
            _uiManager.InputActions.UIController.MousePointer.performed -= CheckMousePosition;
            EventBus.Unsubscribe<ClickInItemSlotEvent>(AddInfoInPopup);
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
                            _itemSlots[i].GetComponent<ItemSlotButton>().SetIsEmpty(false);
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

        private void GenerateInventorySlots()
        {
            for (int i = 0; i < _countsOfLines; i++)
            {
                var newItemLine = Instantiate(_itemLinesPrefab, _inventoryItemSlotContainer.transform);
                var newEmptyLine = Instantiate(_emptyLinesPrefab, _inventoryEmptySlotContainer.transform);

                for (int j = 0; j < _countsOfSlotsInLine; j++)
                {
                    var newItemSlot = Instantiate(_itemSlotPrefab, newItemLine.transform);
                    newItemSlot.GetComponent<ItemSlotButton>().Init(_itemInformationPopup, _itemSlots.Count);
                    _itemSlots.Add(newItemSlot);
                    Instantiate(_emptySlotPrefab, newEmptyLine.transform);
                }
            }
        }

        private void AddInfoInPopup(ClickInItemSlotEvent clickInItemSlotEvent)
        {
            var slotId = clickInItemSlotEvent.SlotId;

            if (slotId < _playerInventory.Inventory.Count && _playerInventory.Inventory.TryGetValue(slotId, out int itemID))
            {
                foreach (var item in _itemDataBase.Items)
                {
                    var idItemInBase = item.ItemID;

                    if ((int)idItemInBase == itemID)
                    {
                        var itemInforfationPopupText = _itemInformationPopup.GetComponentInChildren<TextMeshProUGUI>();

                        var itemName = item.name;
                        var requaremntSRT = item.RequaredSTR;
                        var requaremntINT = item.RequaredINT;
                        var requaremntAGL = item.RequaredAGL;
                        itemInforfationPopupText.text = itemName + "\n" + 
                            "STR: " + requaremntSRT + "\n" +
                            "INT: " + requaremntINT + "\n" +
                            "AGL: " + requaremntAGL;
                    }
                }
            }
        }
    }
}