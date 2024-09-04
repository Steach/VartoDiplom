using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine
{
    public class RunToEnemyAndAttakeState : State
    {
        public RunToEnemyAndAttakeState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);
            
            Character.Agent.speed = Character.CurrentRunSpeed;
            Character.Agent.isStopped = false;
            Character.Agent.SetDestination(Character.PlayerData.Target.transform.position);

            StartRunToTarget();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            StartRunToTarget();
        }

        private void StartRunToTarget()
        {
            if (Character.Agent.remainingDistance < 3)
                Character.Agent.speed = 0;
            else
                Character.Agent.speed = Character.CurrentRunSpeed;

            Character.Animator.SetBool(GameData.PlayerDistanceForAttake, Character.Agent.remainingDistance < 3);
            Character.Animator.SetBool(GameData.PlayerHasTarget, Character.PlayerData.Target != null);
            Character.Animator.SetTrigger(GameData.PlayerRunTargetAndAttake);

            if (Character.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == GameData.PlayerAttakeB1P && 
                    !Character.Animator.IsInTransition(0) && 
                    Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Character.Animator.SetBool(GameData.PlayerHasTarget, false);
                Character.FSM.ChangeState(Character.StateIdle);
            }
        }
    }
}