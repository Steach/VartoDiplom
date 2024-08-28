using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<IncreasingScoreEvent>(IncreasScore);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<IncreasingScoreEvent>(IncreasScore);
    }

    private void IncreasScore(IncreasingScoreEvent increasingScoreEvent)
    {
        _score += increasingScoreEvent.Score;
        _scoreText.text = _score.ToString();
    }
}
