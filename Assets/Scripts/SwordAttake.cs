using Project.Managers.Player;
using System.Collections;
using UnityEngine;

public class SwordAttake : MonoBehaviour
{
    [SerializeField] private int _damage;
    private bool _isDamaged = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerManager>(out PlayerManager playerManager))
            {

                StartCoroutine(DamageEnemy(playerManager));
            }
        }
    }

    IEnumerator DamageEnemy(PlayerManager playerManager)
    {
        if (!_isDamaged) 
        {
            playerManager.PlayerIndicators.GetDamage(_damage);
            Debug.Log(playerManager.PlayerIndicators.CurrentHP);
            _isDamaged = true;
        }

        yield return new WaitForSeconds(1);

        _isDamaged = false;
    }
}
