using Project.Systems.ItemSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Managers.Spawner
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private ItemDataBase _data;
        [SerializeField] private ScriptableItem[] _sword;
        [SerializeField] private GameObject[] _place;

        private void OnEnable()
        {
            EventBus.Subscribe<EnemyDieEvent>(SpawnScriptable);
            for (int i = 0; i < _sword.Length; i++)
                _sword[i].Spawn(_place[i].transform.position);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<EnemyDieEvent>(SpawnScriptable);
        }

        private void SpawnScriptable(EnemyDieEvent enemyDieEvent)
        {
            _data.Items[Random.Range(16, _data.Items.Length)].Spawn(enemyDieEvent.Position);
        }
    }
}