using UnityEngine;

public class SwordAttake : MonoBehaviour
{
    [SerializeField] private int _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.TryGetComponent<EnemySimple>(out EnemySimple enemy))
            {
                enemy.HP = _damage;
            }
            ChangeScore(1);
        }
    }

    private void ChangeScore(int score)
    {
        EventBus.Publish(new IncreasingScoreEvent(score));
    }
}
