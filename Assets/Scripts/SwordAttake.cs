using System.Collections;
using UnityEngine;

public class SwordAttake : MonoBehaviour
{
    [SerializeField] private int _damage;
    private bool _isDamaged = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.TryGetComponent<EnemySimple>(out EnemySimple enemy))
            {

                StartCoroutine(DamageEnemy(enemy));
            }
            ChangeScore(1);
        }
    }

    private void ChangeScore(int score)
    {
        EventBus.Publish(new IncreasingScoreEvent(score));
    }

    IEnumerator DamageEnemy(EnemySimple enemy)
    {
        if (!_isDamaged) 
        {
            Debug.Log($"Enemy HP: {enemy.HP}. Damage: {_damage}");
            enemy.HP = _damage;
            enemy.GetDamage();
            Debug.Log($"Enemy HP: {enemy.HP}. Damage: {_damage}");
            _isDamaged = true;
        }

        yield return new WaitForSeconds(1);

        _isDamaged = false;
    }
}
