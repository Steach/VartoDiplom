using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine
{
    public class AttakeInPlaceState : State
    {
        public AttakeInPlaceState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter();

            Character.Agent.isStopped = true;
            //Character.Animator.Rebind();
            Character.Animator.SetTrigger(GameData.PlayerIdleSword);
            Character.Animator.SetTrigger(GameData.PlayerAttakeB1P);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!Character.Animator.IsInTransition(0) && Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                if (Random.Range(0, 5) == 0)
                {
                    Character.FSM.ChangeState(Character.StateIdle);
                }
                else
                {
                    Character.FSM.ChangeState(Character.StateAttackInPlace);
                } 
            }    
        }

        public override void Exit(object data = null) 
        {
            base.Exit();
        }
    }
}