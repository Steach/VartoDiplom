using Project.Managers.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Systems.StateMachine.Enemy
{
    public class FSMEnemy
    {
        public StateMachine FSM { get; private set; }
        public IdleState IdleState { get; private set; }
        public AttakeState AttackState { get; private set; }
        public MovingState MovingState { get; private set; }
        public DeathState DeathState { get; private set; }
        public TakeDamageState TakeDamageState { get; private set; }
        public EnemyManager EnemyManager { get; private set; }
        public Animator EnemyAnimator { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        public void Init(EnemyManager enemyManager, Animator enemyAnimator, NavMeshAgent agent)
        {
            EnemyManager = enemyManager;
            EnemyAnimator = enemyAnimator;
            Agent = agent;

            FSM = new StateMachine();
            IdleState = new IdleState(this, FSM);
            AttackState = new AttakeState(this, FSM);
            MovingState = new MovingState(this, FSM);
            DeathState = new DeathState(this, FSM);
            TakeDamageState = new TakeDamageState(this, FSM);

            FSM.Init(IdleState);
        }

        public void RunOnUpdate()
        {
            FSM.CurrentEnemyState.LogicUpdate();
        }

        public void RunOnFixedUpdate()
        {
            FSM.CurrentEnemyState.PhysicsUpdate();
        }
    }
}