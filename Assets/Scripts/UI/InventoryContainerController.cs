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
        [Header("Managers")]
        [SerializeField] private UIManager _uiManager;
        [Space]
        [Header("Data base")]
        [SerializeField] private ItemDataBase _itemDataBase;
        [SerializeField] private Sprite _emptySlotIcon;
        [SerializeField] private GameObject[] _equipmentSlots;
        [SerializeField] private Sprite _emptyEquipSlots;
        private List<GameObject> _itemSlots = new List<GameObject>();

        [Space]
        [Header("UI Prefabs")]
        [SerializeField] private GameObject _inventoryEmptySlotContainer;
        [SerializeField] private GameObject _inventoryItemSlotContainer;
        [SerializeField] private GameObject _emptySlotPrefab;
        [SerializeField] private GameObject _itemSlotPrefab;
        [SerializeField] private GameObject _itemLinesPrefab;
        [SerializeField] private GameObject _emptyLinesPrefab;
        [SerializeField] private GameObject _itemInformationPopup;
        [Space]
        [Header("Inventory Configuration")]
        [SerializeField] private int _countsOfSlotsInLine;
        [SerializeField] private int _countsOfLines;

        private const int c_helmetSlotID = 0;
        private const int c_bodySlotID = 1;
        private const int c_legsSlotID = 2;
        private const int c_bootsSlotID = 3;
        private const int c_gauntletsSlotID = 4;
        private const int c_capeSlotID = 5;

        private PlayerInventory _playerInventory;
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
            EventBus.Subscribe<EquipItemEvent>(DeleteItemFromInventory);
            EventBus.Subscribe<UpdateInventoryVisual>(AddItemToInventory);
            //EventBus.Subscribe<DropItemEvent>();
        }

        private void OnDisable()
        {
            _uiManager.InputActions.UIController.MousePointer.performed -= CheckMousePosition;
            EventBus.Unsubscribe<ClickInItemSlotEvent>(AddInfoInPopup);
            EventBus.Unsubscribe<EquipItemEvent>(DeleteItemFromInventory);
            EventBus.Unsubscribe<UpdateInventoryVisual>(AddItemToInventory);
            //EventBus.Unsubscribe<DropItemEvent>();
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

        private void AddItemToInventory(UpdateInventoryVisual updateInventoryVisual)
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

            UpdateEquipmetSlots();
        }

        private void UpdateEquipmetSlots()
        {
            foreach (var equip in _playerInventory.EquipedArmor)
            {
                ArmorType armorType = equip.Key;

                switch (armorType)
                {
                    case ArmorType.Helmet:
                        
                        var imageComponent = _equipmentSlots[c_helmetSlotID].GetComponent<Image>();
                        if (equip.Value == 0)
                            imageComponent.sprite = _emptyEquipSlots;
                        else 
                            imageComponent.sprite = SearchItemInDataBase(equip.Value);
                        break;

                    case ArmorType.Body:
                        
                        imageComponent = _equipmentSlots[c_bodySlotID].GetComponent<Image>();
                        if (equip.Value == 0)
                            imageComponent.sprite = _emptyEquipSlots;
                        else
                            imageComponent.sprite = SearchItemInDataBase(equip.Value);
                        break;

                    case ArmorType.Boot:
                        
                        imageComponent = _equipmentSlots[c_bootsSlotID].GetComponent<Image>();
                        if (equip.Value == 0)
                            imageComponent.sprite = _emptyEquipSlots;
                        else
                            imageComponent.sprite = SearchItemInDataBase(equip.Value);
                        break;

                    case ArmorType.Cape:
                        
                        imageComponent = _equipmentSlots[c_capeSlotID].GetComponent<Image>();
                        if (equip.Value == 0)
                            imageComponent.sprite = _emptyEquipSlots;
                        else
                            imageComponent.sprite = SearchItemInDataBase(equip.Value);
                        break;

                    case ArmorType.Legs:
                        
                        imageComponent = _equipmentSlots[c_legsSlotID].GetComponent<Image>();
                        if (equip.Value == 0)
                            imageComponent.sprite = _emptyEquipSlots;
                        else
                            imageComponent.sprite = SearchItemInDataBase(equip.Value);
                        break;

                    case ArmorType.Gauntlets:
                        
                        imageComponent = _equipmentSlots[c_gauntletsSlotID].GetComponent<Image>();
                        if (equip.Value == 0)
                            imageComponent.sprite = _emptyEquipSlots;
                        else
                            imageComponent.sprite = SearchItemInDataBase(equip.Value);
                        break;
                }
            }    
        }

        private Sprite SearchItemInDataBase(int id)
        {
            Sprite newSprite = _emptyEquipSlots;
            foreach (var item in _itemDataBase.Items)
            {
                var itemId = item.ItemID;
                if ((int)itemId == id)
                {
                    newSprite = item.Icon;
                    return newSprite;
                }
            }
            return newSprite;
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

            if (slotId < _playerInventory.Inventory.Count && _playerInventory.Inventory.TryGetValue(slotId, out int itemID) && itemID != 0)
            {
                foreach (var item in _itemDataBase.Items)
                {
                    var idItemInBase = item.ItemID;

                    if ((int)idItemInBase == itemID)
                    {
                        var itemInforfationPopupText = _itemInformationPopup.GetComponentInChildren<TextMeshProUGUI>();
                        string textForType = ""; 

                        var baseItemText = "\n" +
                            "STR: " + item.RequirementSTR + "\n" +
                            "INT: " + item.RequirementINT + "\n" +
                            "AGL: " + item.RequirementAGL;

                        if (item.ItemType == ItemType.Armor)
                        {
                            //var armorType = item.ItemType.ToString();
                            textForType =
                                item.ArmorType.ToString() + "\n" +
                                "Armor: " + item.Armor + "\n" +
                                "\n" + "Requirements: " + "\n" + 
                                item.GenderType.ToString();
                        }
                        else if (item.ItemType == ItemType.Weapon)
                        {
                            textForType = 
                                "Damage" + item.Damage + "\n" +
                                "Max Attake Distance: " + item.AttakeDistance +
                                "\n" + "Requirements: " + "\n";
                        }

                        itemInforfationPopupText.text = textForType + baseItemText;
                    }
                }
            }
        }

        private void DeleteItemFromInventory(EquipItemEvent equipItemEvent)
        {
            _itemSlots[equipItemEvent.SlotId].GetComponent<Image>().sprite = _emptySlotIcon;
            _itemSlots[equipItemEvent.SlotId].GetComponent<ItemSlotButton>().SetIsEmpty(true);
        }
    }
}