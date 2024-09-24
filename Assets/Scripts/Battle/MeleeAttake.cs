using UnityEngine;

namespace Project.Systems.Battle
{
    public class MeleeAttake : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _attakeParticle;
        [SerializeField] private int _damage;

        private void OnEnable()
        {
            var collisionModule = _attakeParticle.collision;
            collisionModule.enabled = true;
            EventBus.Subscribe<MeleeAttakeEvent>(PerformAttake);
            _attakeParticle.collision.SetPlane(0, null);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<MeleeAttakeEvent>(PerformAttake);
        }

        private void PerformAttake(MeleeAttakeEvent meleeAttakeEvent)
        {
            _damage = meleeAttakeEvent.Damage;
            _attakeParticle.Play();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponentInChildren<EnemySimple>();
                enemy.HP = _damage;
                enemy.GetDamage();
            }
        }
    }
}