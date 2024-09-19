using Project.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.ItemSystem
{
    [CreateAssetMenu(fileName = "ItemsDataBase", menuName = "Items/DataBase", order = 0)]
    public class ItemDataBase : ScriptableObject
    {
        [SerializeField] private ScriptableItem[] _items;

        public ScriptableItem[] Items { get { return _items; } }

        public WeaponType GetWeaponType(int id)
        {
            var weaponType = WeaponType.NotWeapon;
            foreach (var item in _items)
            {
                var itemId = item.ItemID;

                if ((int)itemId == id)
                    weaponType = item.WeaponType;
            }
            return weaponType;
        }
    }
}