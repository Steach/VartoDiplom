using UnityEngine;

namespace Project.Systems.StateMachine.Enemy
{
    public class AttakeState : EnemyStates
    {
        public AttakeState(FSMEnemy characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);

            //Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.AttakeController;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.EnemySimple.Target == null)
                Character.FSM.ChangeState(Character.IdleState);
            else
            {
                Character.Agent.SetDestination(Character.EnemySimple.Target.transform.position);

                if (Character.Agent.remainingDistance <= 1)
                {
                    Character.Agent.isStopped = true;
                    Character.Agent.velocity = Vector3.zero;
                    Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.AttakeController;
                }
                else if (Character.Agent.remainingDistance > 1)
                {
                    Character.Agent.isStopped = false;
                    Character.Agent.SetDestination(Character.EnemySimple.Target.transform.position);
                    Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.MovingController;
                }
            }
        }
    }
}
