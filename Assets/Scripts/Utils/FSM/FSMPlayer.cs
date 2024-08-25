using Project.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Systems.StateMachine
{
    public abstract class FSMCharacter : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected GameData.PlayerData _playerData;
        
        public NavMeshAgent Agent { get { return _agent; } }
        public Animator Animator { get { return _animator; } }
        public GameData.PlayerData PlayerData { get { return _playerData; } }
    }
}