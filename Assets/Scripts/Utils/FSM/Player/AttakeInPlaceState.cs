using Project.Data;
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

            Character.Agent.isStopped = true;
            Character.Animator.SetTrigger(GameData.PlayerAttakeB1P);
            if (_timer == 1)
            {
                CallParticle();
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
                EventBus.Publish<MeleeAttakeEvent>(new MeleeAttakeEvent(damage));
            }
            else if (idLeftHand == (int)ItemsID.Bow || idRightHand == (int)ItemsID.Staff)
            {
                var damage = Character.PlayerManager.PlayerInventory.ItemDataBase.GetDamage(idLeftHand);
                EventBus.Publish(new RangeAttakeEvent(damage, Input.mousePosition));
            }
        }
    }
}