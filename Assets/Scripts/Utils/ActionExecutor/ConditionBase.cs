
using UnityEngine;

namespace Project.Systems.Action
{
    public abstract class ConditionBase : MonoBehaviour
    {
        public abstract bool Check(object data = null);
    }
}