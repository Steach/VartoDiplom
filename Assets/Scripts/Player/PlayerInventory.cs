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
        public Dictionary<int, int> Inventory { get {  return _inventory; } }
        public Dictionary<ArmorType, int> EquipedArmor { get { return _equipedArmor; } }

        public void Init(ItemDataBase itemDataBase) =>
            _itemDataBase = itemDataBase;

        public void OnEnableEvents()
        {
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
            if (_inventory.Count < _maxInvectoryKey)
            {
                _inventory.Add(_inventory.Count, grabItemEvent.ItemID);

                foreach (var inv in _inventory)
                {
                    Debug.Log($"{inv.Key}: {inv.Value}");
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
                        _equipedArmor[item.ArmorType] = currentItemId;
                        EventBus.Publish<UpdateVisualEvent>(new UpdateVisualEvent(true));
                    }
                }
            }
        }

        private void DropItem(DropItemEvent dropItemEvent)
        { }
    }
}