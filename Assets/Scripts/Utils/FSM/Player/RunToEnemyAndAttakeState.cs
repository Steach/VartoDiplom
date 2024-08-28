using Project.Data;
using System.Collections;
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

            //StartRunToTarget();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Character.PlayerData.Target && Vector3.Distance(Character.Agent.transform.position, Character.Agent.destination) >= 3)
            {
                Debug.Log(Vector3.Distance(Character.Agent.transform.position, Character.Agent.destination));
                StartRunToTarget();
            }
            else
            {
                Character.Agent.ResetPath();
                FSM.ChangeState(Character.StateAttackInPlace);
            }
        }

        private void StartRunToTarget()
        {
            Character.Agent.speed = Character.PlayerData.RunSpeed;
            Character.Agent.isStopped = false;
            Character.Animator.SetTrigger(GameData.PlayerRunFrontPlaceSword);
            Character.Agent.SetDestination(Character.PlayerData.Target.transform.position);
        }
    }
}