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
            Debug.Log(weaponTransform.gameObject.name);
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherGO = other.gameObject;

            //if (otherGO.CompareTag(GameData.WeaponTag))
            //{
            //    var rb = other.GetComponent<Rigidbody>();
            //    rb.isKinematic = true;
            //    other.transform.SetParent(_mainWeaponPlace.transform, false);
            //    other.transform.localPosition = new Vector3(0, 0, 0);
            //    other.transform.localRotation = new Quaternion(0, 0, 0, 0);
            //    _mainWeapon = other.gameObject;
            //}
            //
            //if (otherGO.gameObject.CompareTag(GameData.ShieldTag))
            //{
            //    var rb = other.GetComponent<Rigidbody>();
            //    rb.isKinematic = true;
            //    other.transform.SetParent(_shieldPlace.transform, false);
            //    other.transform.localPosition = new Vector3(0, 0, 0);
            //    other.transform.localRotation = new Quaternion(0, 0, 0, 0);
            //    _shield = other.gameObject;
            //}

            if (otherGO.CompareTag(GameData.ArmorTag) || otherGO.CompareTag(GameData.WeaponTag) || otherGO.gameObject.CompareTag(GameData.ShieldTag))
            {
                Debug.Log(otherGO.name);

                if (otherGO.TryGetComponent<ItemCharacteristics>(out ItemCharacteristics itemCharacteristics))
                {
                    ItemsID enumId = itemCharacteristics.ItemID;
                    var intId = (int)enumId;
                    EventBus.Publish<GrabItemEvent>(new GrabItemEvent(intId));
                }

                //otherGO.SetActive(false);
                Destroy(otherGO);
            }
        }
    }
}