using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace Project.Systems.ItemSystem
{
    public class PlayerInventory
    {
        private GameObject _player;
        private int _maxInvectoryKey = 100;
        private ItemDataBase _itemDataBase;
        private Dictionary<int, int> _inventory = new Dictionary<int, int>();
        private Dictionary<ArmorType, int> _equipedArmor = new Dictionary<ArmorType, int>();
        private Dictionary<WeaponType, int> _equipedWeapon = new Dictionary<WeaponType, int>();
        private int _currentArmor = 0;

        public Dictionary<int, int> Inventory { get {  return _inventory; } }
        public Dictionary<ArmorType, int> EquipedArmor { get { return _equipedArmor; } }

        public void Init(ItemDataBase itemDataBase) =>
            _itemDataBase = itemDataBase;

        public void OnEnableEvents()
        {
            GenerateInventory();
            EventBus.Subscribe<GrabItemEvent>(AddItemToInventory);
            EventBus.Subscribe<EquipItemEvent>(EquipItem);
            EventBus.Subscribe<DropItemEvent>(DropItem);
            _equipedArmor.Add(ArmorType.Helmet, 0);
            _equipedArmor.Add(ArmorType.Body, 0);
            _equipedArmor.Add(ArmorType.Boot, 0);
            _equipedArmor.Add(ArmorType.Cape, 0);
            _equipedArmor.Add(ArmorType.Gauntlets, 0);
            _equipedArmor.Add(ArmorType.Legs, 0);
        }

        public void RunOnUpdate()
        { 
        }

        public void OnDisableEvents()
        {
            EventBus.Unsubscribe<GrabItemEvent>(AddItemToInventory);
            EventBus.Unsubscribe<EquipItemEvent>(EquipItem);
            EventBus.Unsubscribe<DropItemEvent>(DropItem);
        }

        private void AddItemToInventory(GrabItemEvent grabItemEvent)
        {
            for (int i = 0; i < _maxInvectoryKey; i++)
            {
                if (_inventory[i] == 0)
                {
                    _inventory[i] = grabItemEvent.ItemID;
                    break;
                }
            }

            foreach (var inv in _inventory)
            {
                Debug.Log($"{inv.Key}: {inv.Value}");
            }
        }

        //TODO: ADD TO WEAPON TO EQUIP ITEM

        private void EquipItem(EquipItemEvent equipItemEvent)
        {
            if (_inventory.TryGetValue(equipItemEvent.SlotId, out int currentItemId))
            {
                foreach (var item in _itemDataBase.Items)
                {
                    var dataItemBaseId = item.ItemID;
                    if ((int)dataItemBaseId == currentItemId)
                    {
                        if (_equipedArmor[item.ArmorType] != 0)
                        {
                            var oldEqupItem = _equipedArmor[item.ArmorType];
                            _equipedArmor[item.ArmorType] = currentItemId;
                            _inventory[equipItemEvent.SlotId] = oldEqupItem;
                        }
                        else
                        {
                            _equipedArmor[item.ArmorType] = currentItemId;
                            _inventory[equipItemEvent.SlotId] = 0;
                        }

                        EventBus.Publish<UpdateInventoryVisual>(new UpdateInventoryVisual(true));
                        EventBus.Publish<UpdateVisualEvent>(new UpdateVisualEvent(true));
                        CalculateCurrentArmor();
                    }
                }
            }
        }

        private void CalculateCurrentArmor()
        {
            var equipedArmor = 0;
            foreach (var armor in _equipedArmor)
            { 
                foreach (var item in _itemDataBase.Items)
                {
                    var itemID = item.ItemID;
                    if (armor.Value == (int)itemID)
                    {
                        equipedArmor += item.Armor;
                    }
                }
            }
            _currentArmor = equipedArmor;

            Debug.Log($"Current Player armor: {_currentArmor}");
        }

        private void DropItem(DropItemEvent dropItemEvent)
        { }

        private void GenerateInventory()
        {
            for (int i = 0; i < _maxInvectoryKey; i++)
                _inventory[i] = 0;
        }
    }
}