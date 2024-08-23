using UnityEngine;

namespace Project.Controllers.CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private Vector3 _offset;

        private void Awake()
        {
        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            var x = _target.transform.position.x;
            var y = _target.transform.position.y;
            var z = _target.transform.position.z;
            transform.position = new Vector3(x + _offset.x, y + _offset.y, z + _offset.z);
        }
    }
}