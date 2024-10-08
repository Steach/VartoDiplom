using UnityEngine;

namespace Project.Systems.Action
{
    public class ExecuteOnButtonPress : ExecutorBase
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Execute();
            }
        }
    }
}