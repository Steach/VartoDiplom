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
            
            Character.Agent.speed = Character.PlayerData.RunSpeed;
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
            //Debug.Log(Vector3.Distance(Character.Agent.transform.position, Character.Agent.destination));

            if (Character.PlayerData.Target && Vector3.Distance(Character.Agent.transform.position, Character.Agent.destination) >= 3)
            {
                Character.Agent.speed = Character.PlayerData.RunSpeed;
                Character.Agent.isStopped = false;
                Character.Animator.SetTrigger(GameData.PlayerRunFrontPlaceSword);
                Character.Agent.SetDestination(Character.PlayerData.Target.transform.position);
            }
            else if (Character.PlayerData.Target && Vector3.Distance(Character.Agent.transform.position, Character.Agent.destination) <= 3)
            {
                Character.Agent.ResetPath();
                Character.Agent.speed = 0;

                Vector3 direction = (Character.PlayerData.Target.transform.position - Character.transform.position).normalized;
                direction.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Character.transform.rotation = Quaternion.Slerp(Character.transform.rotation, lookRotation, Time.deltaTime * 5f);
                Character.Animator.SetTrigger(GameData.PlayerAttakeB1P);

                if (Character.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == GameData.PlayerAttakeB1P && 
                    !Character.Animator.IsInTransition(0) && 
                    Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    Character.FSM.ChangeState(Character.StateIdle);
                }
            }
        }
    }
}