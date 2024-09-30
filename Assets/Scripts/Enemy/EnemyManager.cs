using UnityEngine;

namespace Project.Managers.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySimple[] _enemies;

        private void Awake()
        {
            
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
                enemy.EnemyUpdate();
        }

        private void OnDisable()
        {
            foreach (var enemy in _enemies)
                enemy.EnemyOnDisable();
        }
    }
}