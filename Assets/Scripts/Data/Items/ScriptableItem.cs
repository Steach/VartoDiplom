using UnityEngine;
using UnityEngine.UI;

namespace Project.Systems.ItemSystem
{
    public abstract class ScriptableItem : ScriptableObject
    {
        
        [SerializeField] private ItemsID _itemID;
        [Space]
        [Header("Item Types Configuration")]
        [SerializeField] protected ItemType _itemType;
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private GenderType _genderType;
        [Space]
        [Header("Item Visual")]
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Sprite _icon;
        [Space]
        [Header("Item Requarment")]
        [SerializeField] protected int _requaredSTR;
        [SerializeField] protected int _requaredINT;
        [SerializeField] protected int _requaredAGL;
        [Space]
        [Header("Item Characteristics")]
        [SerializeField] protected int _damage;
        [SerializeField] protected float _attakeDistance;
        [SerializeField] protected int _armor;
        public ItemsID ItemID { get { return _itemID;} }
        public ItemType ItemType { get { return _itemType; } }
        public ArmorType ArmorType { get { return _armorType; } }
        public GenderType GenderType { get { return _genderType; } }
        public int Damage { get { return _damage; } }
        public float AttakeDistance {  get { return _attakeDistance; } }
        public int Armor { get { return _armor; } }
        public Sprite Icon { get { return _icon; } }
        public int RequirementSTR {  get { return _requaredSTR; } }
        public int RequirementINT {  get { return _requaredINT; } }
        public int RequirementAGL { get { return _requaredAGL; } }

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