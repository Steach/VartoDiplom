using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.ItemSystem
{
    public class PlayerInventory
    {
        private GameObject _player;
        private int _maxInvectoryKey = 30;
        private Dictionary<int, int> _inventory = new Dictionary<int, int>();

        public void OnEnableEvents()
        {
            EventBus.Subscribe<GrabItemEvent>(AddItemToInventory);
        }

        public void RunOnUpdate()
        { 
        }

        public void OnDisableEvents()
        {
            EventBus.Unsubscribe<GrabItemEvent>(AddItemToInventory);
        }

        private void AddItemToInventory(GrabItemEvent grabItemEvent)
        {
            if (_inventory.Count < 30)
            {
                _inventory.Add(_inventory.Count, grabItemEvent.ItemID);

                foreach (var inv in _inventory)
                {
                    Debug.Log($"{inv.Key}: {inv.Value}");
                }
            }
        }
    }
}