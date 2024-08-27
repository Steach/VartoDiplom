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

            if (data != null && data is float)
            {

            }
        }
    }
}