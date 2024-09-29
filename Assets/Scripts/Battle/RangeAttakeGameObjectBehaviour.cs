using Project.Systems.Pooller;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Systems.Battle
{
    public class RangeAttakeGameObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _lifeTime;

        [SerializeField] private float _velocity;

        private TrailRenderer _trailRenderer;
        private ParticleSystem _particleSystem;
        private Transform _transform;
        private Vector3 _direction;

        private bool _isHit = false;

        public void Init(int damage, Vector3 mousePosition)
        {
            var collisionModule = GetComponent<ParticleSystem>().collision;
            collisionModule.enabled = true;
            collisionModule.SetPlane(0, null);
            _damage = damage;
            _isHit = false;
            var newMousePosition = new Vector3(mousePosition.x, transform.position.y, mousePosition.z);
            _direction = (newMousePosition - transform.position).normalized;
        }

        private void OnEnable()
        {
            _trailRenderer = gameObject.GetComponent<TrailRenderer>();
            _particleSystem = gameObject.GetComponent<ParticleSystem>();
            _transform = gameObject.GetComponent<Transform>();
            

            if (_trailRenderer != null) _trailRenderer.Clear();
            if (_particleSystem != null) _particleSystem.Clear();

            _particleSystem.Play();

            StartCoroutine(DisableObjectAndReturnToPool());
        }


        private void Update()
        {
            if (!_isHit)
            {
                transform.Translate(_direction * _velocity * Time.deltaTime);

                RaycastHit _hit;
                if (Physics.Raycast(_transform.position, _transform.forward, out _hit, 2 + (_velocity * 0.02f)))
                {
                    Collider _target = _hit.collider;
                    Debug.Log(_target.gameObject.tag);
                    if (_target.CompareTag("Enemy"))
                    {
                        _isHit = true;
                        var enemy = _target.GetComponentInChildren<EnemySimple>();
                        enemy.HP = _damage;
                        enemy.GetDamage();
                    }
                }
            }
            else
            {
                DisableAndReturnToPool();
            }
        }

        private void OnDisable()
        {
            
        }

        

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (other.CompareTag("Enemy"))
                {
                    var enemy = other.GetComponentInChildren<EnemySimple>();
                    enemy.HP = _damage;
                    enemy.GetDamage();
                }
            }

            DisableAndReturnToPool();
        }

        IEnumerator DisableObjectAndReturnToPool()
        {
            yield return new WaitForSeconds(_lifeTime);
            DisableAndReturnToPool();
        }

        private void DisableAndReturnToPool()
        {
            ObjectPool.Instance.ReturnToPool(this.gameObject);
            gameObject.SetActive(false);
        }
    }
}