using Project.Systems.Pooller;
using UnityEngine;

namespace Project.Systems.Battle
{
    public class RangeAttake : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _attakeParticle;
        [SerializeField] private Transform _shootPointTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private int _damage;
        private void OnEnable() =>
            EventBus.Subscribe<RangeAttakeEvent>(Shoot);

        private void OnDisable() =>
            EventBus.Unsubscribe<RangeAttakeEvent>(Shoot);

        private void Shoot(RangeAttakeEvent rangeAttakeEvent)
        {
            var shootPosition = new Vector3(_shootPointTransform.position.x + _offset.x, _shootPointTransform.position.y + _offset.y, _shootPointTransform.position.z + _offset.z);

            var rangeAttkeParticle = ObjectPool.Instance.GetObjects(_attakeParticle.gameObject, shootPosition, _shootPointTransform.rotation);
            var behavior = rangeAttkeParticle.GetComponent<RangeAttakeGameObjectBehaviour>();
            Ray ray = Camera.main.ScreenPointToRay(rangeAttakeEvent.MouseHitPotision);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                behavior.Init(rangeAttakeEvent.Damage, hit.point);
            }
        }
    }
}