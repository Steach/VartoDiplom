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

        public bool CheckWeaponType(int id)
        {
            foreach (var item in _items)
            {
                var itemID = item.ItemID;

                if ((int)itemID == id)
                    return true;
            }

            return false;
        }

        public float GetWeaponAttakeDistance(int id)
        {
            var dist = 0f;

            foreach (var item in _items)
            {
                var itemID = item.ItemID;

                if ((int)itemID == id)
                    dist = item.AttakeDistance;
            }

            return dist;
        }

        public int GetDamage(int id)
        {
            var damage = 0;
            foreach (var item in _items)
            {
                var itemID = item.ItemID;

                if((int)itemID == id)
                    damage = item.Damage;
            }

            return damage;
        }

        public int GetUsedStamina(int id)
        {
            var stamina = 0;

            foreach (var item in _items)
            {
                var itemID = item.ItemID;

                if((int)itemID == id)
                    stamina = item.Stamina;
            }

            return stamina;
        }
    }
}