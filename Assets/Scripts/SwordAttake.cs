using UnityEngine;

public class SwordAttake : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.TryGetComponent<EnemySimple>(out EnemySimple enemy))
            {
                enemy.HP = 10;
            }
            ChangeScore(1);
        }
    }

    private void ChangeScore(int score)
    {
        EventBus.Publish(new IncreasingScoreEvent(score));
    }
}
