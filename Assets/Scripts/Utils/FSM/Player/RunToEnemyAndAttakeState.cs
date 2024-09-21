using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine
{
    public class RunToEnemyAndAttakeState : State
    {

        private float _distance = 1;
        public RunToEnemyAndAttakeState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter(data);
            
            Character.Agent.speed = Character.PlayerManager.PlayerCharacteristicsData.CurrentRunSpeed;
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
            if (Character.Agent.remainingDistance < _distance)
            {
                Character.Agent.speed = 0;
                Character.Agent.velocity = Vector3.zero;
                Character.Agent.isStopped = true;
            }
            else
            {
                Character.Agent.speed = Character.PlayerManager.PlayerCharacteristicsData.CurrentRunSpeed;
                Character.Agent.isStopped = false;
            }
               

            Character.Animator.SetBool(GameData.PlayerDistanceForAttake, Character.Agent.remainingDistance < _distance);
            Character.Animator.SetBool(GameData.PlayerHasTarget, Character.PlayerData.Target != null);
            Character.Animator.SetTrigger(GameData.PlayerRunTargetAndAttake);

            if (Character.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == GameData.PlayerAttakeB1P && 
                    !Character.Animator.IsInTransition(0) && 
                    Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Debug.Log("BOW SHOOT");
                Character.Agent.ResetPath();
                Character.Animator.SetBool(GameData.PlayerHasTarget, false);
                Character.FSM.ChangeState(Character.StateIdle);
            }
        }
    }
}