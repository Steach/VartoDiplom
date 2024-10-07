using System.Collections.Generic;
using UnityEngine;

namespace Project.Managers.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [Header("Enemies")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<EnemySimple> _enemies = new List<EnemySimple>();
        [SerializeField] private List<Transform> _spawnSpots = new List<Transform>();
        [Space]
        [Header("Animation Controllers")]
        [SerializeField] private RuntimeAnimatorController _idleController;
        [SerializeField] private RuntimeAnimatorController _attakeController;
        [SerializeField] private RuntimeAnimatorController _movingController;
        [SerializeField] private RuntimeAnimatorController _deathController;
        [SerializeField] private RuntimeAnimatorController _takeDamageController;

        public RuntimeAnimatorController IdleController { get { return _idleController; } }
        public RuntimeAnimatorController AttakeController { get { return _attakeController; } }
        public RuntimeAnimatorController MovingController { get { return _movingController; } }
        public RuntimeAnimatorController DeathController { get { return _deathController; } }
        public RuntimeAnimatorController TakeDamageController { get { return _takeDamageController; } }

        private void Awake()
        {
            SpawnEnemies();
            foreach (var enemy in _enemies)
                enemy.Init(this);
        }

        private void OnEnable()
        {
            foreach (var enemy in _enemies)
                enemy.EnemyOnEnable();
        }

        private void Start()
        {
            foreach (var enemy in _enemies)
                enemy.EnemyStart();
        }

        private void Update()
        {
            foreach (var enemy in _enemies)
            {
                if(enemy.gameObject.activeInHierarchy)
                    enemy.EnemyUpdate();
            }
        }

        private void OnDisable()
        {
            foreach (var enemy in _enemies)
                enemy.EnemyOnDisable();
        }

        private void SpawnEnemies()
        {
            foreach (var spot in _spawnSpots)
            {
                var newEnemy = Instantiate(_enemyPrefab, spot.position, Quaternion.identity, this.transform);
                if (newEnemy.TryGetComponent<EnemySimple>(out EnemySimple enemySimpleComponent))
                    _enemies.Add(enemySimpleComponent);
            }
        }
    }
}