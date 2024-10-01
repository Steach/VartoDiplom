using Project.Data;
using UnityEngine;

namespace Project.Systems.StateMachine.Player
{
    public class RunToEnemyAndAttakeState : PlayerStates
    {

        private float _timer = 0.5f;
        private float _maxTimer = 1.3f;
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
            if (Character.Agent.remainingDistance < CheckPlayerWeaponDistance())
            {
                Character.Agent.speed = 0;
                Character.Agent.velocity = Vector3.zero;
                Character.Agent.isStopped = true;
                if (_timer >= _maxTimer)
                {
                    CallParticle();
                    _timer = 0;
                }
                else if (_timer < _maxTimer)
                {
                    _timer += Time.deltaTime;
                }
                
            }
            else
            {
                Character.Agent.speed = Character.PlayerManager.PlayerCharacteristicsData.CurrentRunSpeed;
                Character.Agent.isStopped = false;
            }
               

            Character.Animator.SetBool(GameData.PlayerDistanceForAttake, Character.Agent.remainingDistance < CheckPlayerWeaponDistance());
            Character.Animator.SetBool(GameData.PlayerHasTarget, Character.PlayerData.Target != null);
            Character.Animator.SetTrigger(GameData.PlayerRunTargetAndAttake);

            if (Character.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == GameData.PlayerAttakeB1P && 
                    !Character.Animator.IsInTransition(0) && 
                    Character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Character.Agent.ResetPath();
                Character.Animator.SetBool(GameData.PlayerHasTarget, false);
                Character.FSM.ChangeState(Character.StateIdle);
            }
        }

        private float CheckPlayerWeaponDistance()
        {
            if (Character.PlayerManager.PlayerInventory.EquipedWeapon != null && Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.LeftHandIndex] != (int)ItemsID.Bow)
            {
                var dist = Character.PlayerManager.PlayerInventory.ItemDataBase.GetWeaponAttakeDistance(Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.RightHandIndex]);
                return dist;
            }

            else if (Character.PlayerManager.PlayerInventory.EquipedWeapon != null && Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.LeftHandIndex] == (int)ItemsID.Bow)
            {
                var dist = Character.PlayerManager.PlayerInventory.ItemDataBase.GetWeaponAttakeDistance(Character.PlayerManager.PlayerInventory.EquipedWeapon[GameData.LeftHandIndex]);
                return dist;
            }
                
            else
                return 1;
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