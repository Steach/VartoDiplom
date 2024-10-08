using Project.Systems.ItemSystem;
using UnityEngine;

namespace Project.Systems.Action
{
    public class SpawnAction : ActionBase
    {
        [SerializeField] private GameObject _parentGameObject;
        [SerializeField] private ScriptableItem[] _scriptableItems;
        [SerializeField] private Transform[] _spawnPositions;
        public override void Execute(object data = null)
        {
            for (int i = 0; i < _spawnPositions.Length; i++)
            {
                _scriptableItems[i].Spawn(_spawnPositions[i].position, _parentGameObject.transform);
            }
        }
    }
}