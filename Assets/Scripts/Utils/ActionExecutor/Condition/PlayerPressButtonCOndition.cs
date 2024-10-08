using UnityEngine;

namespace Project.Systems.Action
{
    public class PlayerPressButtonCOndition : ConditionBase
    {
        private bool _pressed = false;
        public override bool Check(object data = null)
        {
            return _pressed;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _pressed = true;
            }
        }
    }
}