using Project.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.ItemSystem
{
    [CreateAssetMenu(fileName = "ItemsDataBase", menuName = "Items/DataBase", order = 0)]
    public class ItemDataBase : ScriptableObject
    {
        //[SerializeField] public ItemsData[] _items;
        [SerializeField] private ScriptableItem[] _items;

        public ScriptableItem[] Items { get { return _items; } }

        //[System.Serializable]
        //public struct ItemsData
        //{
        //    public ItemsID ItemsID;
        //    public ScriptableItem Item;
        //}
    }
}