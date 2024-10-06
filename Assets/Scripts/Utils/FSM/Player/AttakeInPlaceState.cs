using Project.Data;
using Project.Managers.Player;
using UnityEngine;

namespace Project.Systems.StateMachine.Player
{
    public class AttakeInPlaceState : PlayerStates
    {
        private float _timer = 1;

        public AttakeInPlaceState(FSMPlayer characters, StateMachine FSM) : base(characters, FSM)
        {
        }

        public override void Enter(object data = null)
        {
            base.Enter();


            if (_timer == 1 && CheckStamina())
            {
                CallParticle();
                Character.Agent.isStopped = true;
                Character.Animator.SetTrigger(GameData.PlayerAttakeB1P);
                _timer = 0;
            }
            else if (_timer < 1)
            {
                _timer += Time.deltaTime;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            CheckStamina();

            if (!Character.Animator.IsInTransition(0) && Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !Character.PlayerManager.PlayerController.IsFightInPlace)
            {
                Character.FSM.ChangeState(Character.StateIdle);
            }
        }

        public override void Exit(object data = null) 
        {
            base.Exit();
            _timer = 1;
        }

        private void CallParticle()
        {
            var idRightHand = Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.RightHandIndex];
            var idLeftHand = Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.LeftHandIndex];
            if (idRightHand == (int)ItemsID.TwoHandSword || idRightHand == (int)ItemsID.OneHandSword)
            {
                var damage = Character.PlayerManager.PlayerInventory.ItemDataBase.GetDamage(idRightHand);
                var stamina = Character.PlayerManager.PlayerInventory.ItemDataBase.GetUsedStamina(idRightHand);
                EventBus.Publish<MeleeAttakeEvent>(new MeleeAttakeEvent(damage, stamina));
            }
            else if (idLeftHand == (int)ItemsID.Bow || idRightHand == (int)ItemsID.Staff)
            {
                var damage = Character.PlayerManager.PlayerInventory.ItemDataBase.GetDamage(idLeftHand);
                var stamina = Character.PlayerManager.PlayerInventory.ItemDataBase.GetUsedStamina(idLeftHand);
                EventBus.Publish(new RangeAttakeEvent(damage, Input.mousePosition, stamina));
            }
        }

        private bool CheckStamina()
        {
            var idRightHand = Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.RightHandIndex];
            var idLeftHand = Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.LeftHandIndex];
            var currentStamina = Character.PlayerManager.PlayerIndicators.CurrentST;
            var usedStamina = 0;

            if (idRightHand == (int)ItemsID.TwoHandSword || idRightHand == (int)ItemsID.OneHandSword)
                usedStamina = Character.PlayerManager.PlayerInventory.ItemDataBase.GetUsedStamina(idRightHand);
            else if (idLeftHand == (int)ItemsID.Bow || idRightHand == (int)ItemsID.Staff)
                usedStamina = Character.PlayerManager.PlayerInventory.ItemDataBase.GetUsedStamina(idLeftHand);


            if (currentStamina < usedStamina)
            {
                Character.FSM.ChangeState(Character.StateIdle);
                return false;
            }
            else
                return true;
        }
    }
}