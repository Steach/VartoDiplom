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
        [SerializeField] public ScriptableItem[] Items;

        [System.Serializable]
        public struct ItemsData
        {
            public ItemsID ItemsID;
            public ScriptableItem Item;
        }
    }
}