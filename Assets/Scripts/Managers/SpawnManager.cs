using Project.Systems.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Managers.Spawner
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private ScriptableItem[] _objectsToSpawn;

        private void OnEnable()
        {
            EventBus.Subscribe<EnemyDieEvent>(SpawnScriptable);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyDieEvent>(SpawnScriptable);
        }

        private void SpawnScriptable(EnemyDieEvent enemyDieEvent)
        {
            _objectsToSpawn[Random.Range(16, _objectsToSpawn.Length - 1)].Spawn(enemyDieEvent.Position);
        }
    }
}