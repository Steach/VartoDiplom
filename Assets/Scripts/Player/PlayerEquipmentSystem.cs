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
            if (other.gameObject.CompareTag("Sword"))
            {
                var rb = other.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                other.transform.SetParent(_mainWeaponPlace.transform, false);
                other.transform.localPosition = new Vector3(0, 0, 0);
                other.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _mainWeapon = other.gameObject;
            }

            if (other.gameObject.CompareTag("Shield"))
            {
                var rb = other.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                other.transform.SetParent(_shieldPlace.transform, false);
                other.transform.localPosition = new Vector3(0, 0, 0);
                other.transform.localRotation = new Quaternion(0, 0, 0, 0);
                _shield = other.gameObject;
            }
        }
    }
}