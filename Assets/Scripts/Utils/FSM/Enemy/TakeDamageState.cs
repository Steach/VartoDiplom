using UnityEngine;

namespace Project.Systems.StateMachine.Enemy
{
    public class TakeDamageState : EnemyStates
    {
        public TakeDamageState(FSMEnemy characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);

            Character.EnemyAnimator.runtimeAnimatorController = Character.EnemyManager.TakeDamageController;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!Character.EnemyAnimator.IsInTransition(0) && Character.EnemyAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Character.FSM.ChangeState(Character.IdleState);
            }

            if (Character.EnemySimple.Target != null)
            {
                Character.FSM.ChangeState(Character.AttackState);
            }
        }
    }
}
