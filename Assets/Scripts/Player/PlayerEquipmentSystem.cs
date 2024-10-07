using Project.Data;
using UnityEngine;

namespace Project.Systems.EquipmentSystem
{
    public class PlayerEquipmentSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _mainWeaponPlace;
        [SerializeField] private GameObject _shieldPlace;
        private GameObject _mainWeapon;
        private GameObject _shield;

        private void Start()
        {
            var weaponTransform = _mainWeaponPlace.GetComponentInChildren<Transform>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherGO = other.gameObject;

            if (otherGO.CompareTag(GameData.ArmorTag) || otherGO.CompareTag(GameData.WeaponTag) || otherGO.gameObject.CompareTag(GameData.ShieldTag))
            {
                if (otherGO.TryGetComponent<ItemCharacteristics>(out ItemCharacteristics itemCharacteristics))
                {
                    ItemsID enumId = itemCharacteristics.ItemID;
                    var intId = (int)enumId;
                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(intId));
                }
                Destroy(otherGO);
            }
        }
    }
}