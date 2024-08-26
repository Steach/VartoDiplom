using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine
{
    public class AttakeState : State
    {
        public AttakeState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Character.Agent.isStopped = true;
            Character.Animator.SetTrigger(GameData.PlayerAttakeB1P);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                Character.FSM.ChangeState(Character.StateIdle);

        }

        public override void Exit() 
        {
            base.Exit();
        }
    }
}