using UnityEngine;
using UnityEngine.UI;

namespace Project.Systems.ItemSystem
{
    public abstract class ScriptableItem : ScriptableObject
    {
        [SerializeField] protected ItemType _itemType;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected int _requaredSTR;
        [SerializeField] protected int _requaredINT;
        [SerializeField] protected int _requaredAGL;
        [SerializeField] private ItemsID _itemID;
        public ItemsID ItemID { get { return _itemID;} }
        public Sprite Icon { get { return _icon; } }
        public int RequaredSTR {  get { return _requaredSTR; } }
        public int RequaredINT {  get { return _requaredINT; } }
        public int RequaredAGL { get { return _requaredAGL; } }

        public virtual void Spawn(Vector3 position)
        {
            var positionToSpawn = new Vector3(position.x, 0, position.z);
            var SpawnerItem = Instantiate(_prefab, position, Quaternion.identity);
            SpawnerItem.tag = _itemType.ToString();
            var spawnedItemRB = SpawnerItem.AddComponent<Rigidbody>();
            spawnedItemRB.isKinematic = true;
            SpawnerItem.AddComponent<CapsuleCollider>();
            var ItemChars = SpawnerItem.AddComponent<ItemCharacteristics>();
            ItemChars.Init(_itemID, _itemType);
        }
    }
}