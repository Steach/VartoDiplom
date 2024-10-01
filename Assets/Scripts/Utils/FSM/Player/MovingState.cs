using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine.Player
{
    public class MovingState : PlayerStates
    {
        public MovingState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM) {}

        public override void Enter(object data = null)
        {
            base.Enter(data);

            PlayerMovingConditions();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Character.Agent.hasPath && Character.Agent.velocity.sqrMagnitude > 0)
                PlayerMovingConditions();
            else if (!Character.Agent.hasPath && Character.Agent.velocity.sqrMagnitude <= 0)
                FSM.ChangeState(Character.StateIdle);
        }

        private void PlayerMovingConditions()
        {
            if (Character.PlayerData.IsRunning)
                Character.Agent.speed = Character.PlayerManager.PlayerCharacteristicsData.CurrentRunSpeed;
            else if (!Character.PlayerData.IsRunning)
                Character.Agent.speed = Character.PlayerManager.PlayerCharacteristicsData.CurrentWalkSpeed;

            Character.Agent.isStopped = false;
            Character.Animator.SetTrigger(GameData.PlayerMovingTrigger);
            Character.Animator.SetBool(GameData.PlayerSpeedIsRun, Character.PlayerData.IsRunning);
            Character.Animator.SetBool(GameData.PlayerSpeedIsWalk, !Character.PlayerData.IsRunning);
            Character.Animator.SetInteger(GameData.RightWeaponType, Character.PlayerManager.PlayerInventory.EquipedWeapon[0]);
            Character.Animator.SetInteger(GameData.LeftWeaponType, Character.PlayerManager.PlayerInventory.EquipedWeapon[1]);
        }
    }
}