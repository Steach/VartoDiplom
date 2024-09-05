//using Project.Data;
//using UnityEngine;
//
//namespace Project.Systems.StateMachine
//{
//    public class RunState : State
//    {
//        public RunState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
//        {
//        }
//
//        public override void Enter(object data = null)
//        {
//            base.Enter();
//
//            Character.Agent.isStopped = false;
//            Character.Agent.speed = Character.CurrentRunSpeed;
//            Character.Animator.SetTrigger(GameData.PlayerRunFrontPlaceSword);
//        }
//
//        public override void LogicUpdate()
//        {
//            base.LogicUpdate();
//
//            if (Character.Agent.velocity.sqrMagnitude <= 0 && !Character.Agent.hasPath)
//                FSM.ChangeState(Character.StateIdle);
//            else if (Character.Agent.speed <= Character.CurrentWalkSpeed && Character.Agent.velocity.sqrMagnitude > 0)
//                FSM.ChangeState(Character.StateWalk);
//            else if (Character.Agent.speed >= Character.CurrentWalkSpeed &&
//                Character.Agent.velocity.sqrMagnitude > 0 &&
//                !Character.Animator.IsInTransition(0) &&
//                Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
//                Character.Animator.SetTrigger(GameData.PlayerRunFrontPlaceSword);
//        }
//    }
//}
//
//