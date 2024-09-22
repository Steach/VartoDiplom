using Project.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.ItemSystem
{
    public class PlayerInventory
    {
        private GameObject _player;
        private int _maxInvectoryKey = 100;
        private ItemDataBase _itemDataBase;
        private Dictionary<int, int> _inventory = new Dictionary<int, int>();
        private Dictionary<ArmorType, int> _equipedArmor = new Dictionary<ArmorType, int>();
        private Dictionary<int, int> _equipedWeapon = new Dictionary<int, int>();
        private int _currentArmor = 0;
        private int _rightHandIndex = GameData.RightHandIndex;
        private int _leftHandIndex = GameData.LeftHandIndex;

        public Dictionary<int, int> Inventory { get {  return _inventory; } }
        public Dictionary<ArmorType, int> EquipedArmor { get { return _equipedArmor; } }
        public Dictionary<int, int> EquipedWeapon { get { return _equipedWeapon; } }
        public ItemDataBase ItemDataBase {  get { return _itemDataBase; } }

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
            _equipedWeapon.Add(_rightHandIndex, 0);
            _equipedWeapon.Add(_leftHandIndex, 0);
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
        }

        private void EquipItem(EquipItemEvent equipItemEvent)
        {
            if (_inventory.TryGetValue(equipItemEvent.SlotId, out int currentItemId))
            {
                foreach (var item in _itemDataBase.Items)
                {
                    var dataItemBaseId = item.ItemID;

                    if ((int)dataItemBaseId == currentItemId)
                    {
                        if (item.ItemType == ItemType.Armor)
                        {
                            if (_equipedArmor[item.ArmorType] != 0)
                            {
                                var oldEqupItem = _equipedArmor[item.ArmorType];
                                _equipedArmor[item.ArmorType] = currentItemId;
                                _inventory[equipItemEvent.SlotId] = 0;
                                EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldEqupItem));
                            }
                            else
                            {
                                _equipedArmor[item.ArmorType] = currentItemId;
                                _inventory[equipItemEvent.SlotId] = 0;
                                _inventory[equipItemEvent.SlotId] = 0;
                            }
                        }
                        else if (item.ItemType == ItemType.Weapon)
                        {
                            if (_equipedWeapon[_rightHandIndex] != 0 && _equipedWeapon[_leftHandIndex] != 0)
                            {
                                if (item.WeaponType == WeaponType.TwoHandSword || item.WeaponType == WeaponType.Staff)
                                {
                                    var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                    var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _equipedWeapon[_leftHandIndex] = 0;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.Bow)
                                {
                                    var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                    var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                    _equipedWeapon[_rightHandIndex] = 0;
                                    _equipedWeapon[_leftHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.OneHandSword)
                                {
                                    var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;

                                    if (_equipedWeapon[_leftHandIndex] == (int)ItemsID.Bow)
                                    {
                                        var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                        _equipedWeapon[_leftHandIndex] = 0;
                                        EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                    }

                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.Shield)
                                {
                                    foreach (var item2 in _itemDataBase.Items)
                                    {
                                        var idItemInBase = item2.ItemID;
                                        if ((int)idItemInBase == _equipedWeapon[_rightHandIndex])
                                        {
                                            if (item2.WeaponType == WeaponType.TwoHandSword || item2.WeaponType == WeaponType.Bow || item2.WeaponType == WeaponType.Staff)
                                            {
                                                var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                                var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                                _equipedWeapon[_rightHandIndex] = 0;
                                                _equipedWeapon[_leftHandIndex] = currentItemId;
                                                _inventory[equipItemEvent.SlotId] = 0;
                                                EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                                EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                            }
                                            else if (item2.WeaponType == WeaponType.OneHandSword)
                                            {
                                                var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                                _equipedWeapon[_leftHandIndex] = currentItemId;
                                                EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                            }
                                        }
                                    }
                                }
                            }
                            else if (_equipedWeapon[_rightHandIndex] == 0 && _equipedWeapon[_leftHandIndex] != 0)
                            {
                                if (item.WeaponType == WeaponType.TwoHandSword || item.WeaponType == WeaponType.Staff)
                                {
                                    var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                    _equipedWeapon[_leftHandIndex] = 0;
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.Bow)
                                {
                                    var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                    _equipedWeapon[_leftHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.OneHandSword)
                                {
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    if (_equipedWeapon[_leftHandIndex] == (int)ItemsID.Bow)
                                    {
                                        var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                        _equipedWeapon[_leftHandIndex] = 0;
                                        EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                    }
                                    _inventory[equipItemEvent.SlotId] = 0;
                                }
                                else if (item.WeaponType == WeaponType.Shield)
                                {
                                    var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                    _equipedWeapon[_leftHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                }
                            }
                            else if (_equipedWeapon[_rightHandIndex] != 0 && _equipedWeapon[_leftHandIndex] == 0)
                            {
                                if (item.WeaponType == WeaponType.TwoHandSword || item.WeaponType == WeaponType.Staff)
                                {
                                    var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.Bow)
                                {
                                    var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                    _equipedWeapon[_rightHandIndex] = 0;
                                    _equipedWeapon[_leftHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.OneHandSword)
                                {
                                    var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;

                                    if (_equipedWeapon[_leftHandIndex] == (int)ItemsID.Bow)
                                    {
                                        var oldLeftEquipedItem = _equipedWeapon[_leftHandIndex];
                                        _equipedWeapon[_leftHandIndex] = 0;
                                        EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldLeftEquipedItem));
                                    }

                                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                }
                                else if (item.WeaponType == WeaponType.Shield)
                                {
                                    foreach (var item2 in _itemDataBase.Items)
                                    {
                                        var idItemInBase = item2.ItemID;
                                        if ((int)idItemInBase == _equipedWeapon[_rightHandIndex])
                                        {
                                            if (item2.WeaponType == WeaponType.TwoHandSword || item2.WeaponType == WeaponType.Bow || item2.WeaponType == WeaponType.Staff)
                                            {
                                                var oldRightEquipedItem = _equipedWeapon[_rightHandIndex];
                                                _equipedWeapon[_rightHandIndex] = 0;
                                                _equipedWeapon[_leftHandIndex] = currentItemId;
                                                _inventory[equipItemEvent.SlotId] = 0;
                                                EventBus.Publish<GrabItemEvent>(new GrabItemEvent(oldRightEquipedItem));
                                            }
                                            else if (item2.WeaponType == WeaponType.OneHandSword)
                                            {
                                                _equipedWeapon[_leftHandIndex] = currentItemId;
                                                _inventory[equipItemEvent.SlotId] = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (_equipedWeapon[_rightHandIndex] == 0 && _equipedWeapon[_leftHandIndex] == 0)
                            {
                                if (item.WeaponType == WeaponType.TwoHandSword || item.WeaponType == WeaponType.Staff)
                                {
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                }
                                else if (item.WeaponType == WeaponType.Bow)
                                {
                                    _equipedWeapon[_leftHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                }
                                else if (item.WeaponType == WeaponType.OneHandSword)
                                {
                                    _equipedWeapon[_rightHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                }
                                else if (item.WeaponType == WeaponType.Shield)
                                {
                                    _equipedWeapon[_leftHandIndex] = currentItemId;
                                    _inventory[equipItemEvent.SlotId] = 0;
                                }
                            }
                        }

                        EventBus.Publish<UpdateVisualEvent>(new UpdateVisualEvent(true));
                        CalculateCurrentArmor();
                    }
                }
            }

            SetWeaponBool();
        }

        private void SetWeaponBool()
        {
            WeaponType weaponTypeRH = _itemDataBase.GetWeaponType(_equipedWeapon[_rightHandIndex]);
            WeaponType weaponTypeLH = _itemDataBase.GetWeaponType(_equipedWeapon[_leftHandIndex]);
            Debug.Log((int)weaponTypeRH);
            Debug.Log((int)weaponTypeLH);
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