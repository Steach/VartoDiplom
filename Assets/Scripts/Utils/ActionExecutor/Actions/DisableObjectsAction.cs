using UnityEngine;

namespace Project.Systems.Action
{
    public class DisableObjectsAction : ActionBase
    {
        [SerializeField] private GameObject[] _objectsForDisable;
        public override void Execute(object data = null)
        {
            for (int i = 0; i < _objectsForDisable.Length; i++)
            {
                _objectsForDisable[i].SetActive(false);
            }
        }
    }
}