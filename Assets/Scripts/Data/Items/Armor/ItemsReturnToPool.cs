using Project.Systems.Pooller;
using UnityEngine;

public class ItemsReturnToPool : MonoBehaviour
{
    private void OnDisable()
    {
        //ObjectPool.Instance.ReturnToPool(this.gameObject);
    }
}
