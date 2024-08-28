using UnityEngine;

public class SwordAttake : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            ChangeScore(1);
        }
    }

    private void ChangeScore(int score)
    {
        EventBus.Publish(new IncreasingScoreEvent(score));
    }
}
