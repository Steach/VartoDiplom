using UnityEngine;

namespace Project.Systems.Action
{
    public abstract class ActionBase : MonoBehaviour
    {
        public abstract void Execute(object data = null);
    }
}