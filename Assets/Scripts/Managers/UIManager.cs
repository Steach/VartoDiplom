using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _enemySlider;
    [SerializeField] private Slider _playerExp;
    private int _score = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<IncreasingScoreEvent>(IncreasScore);
        EventBus.Subscribe<HpChangedEvent>(ChangeEnemyHP);
        EventBus.Subscribe<ChangePlayerExperienceEvent>(ChangePlayerExpSlider);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<IncreasingScoreEvent>(IncreasScore);
        EventBus.Unsubscribe<HpChangedEvent>(ChangeEnemyHP);
        EventBus.Unsubscribe<ChangePlayerExperienceEvent>(ChangePlayerExpSlider);
    }

    private void IncreasScore(IncreasingScoreEvent increasingScoreEvent)
    {
        _score += increasingScoreEvent.Score;
        _scoreText.text = _score.ToString();
    }

    private void ChangeEnemyHP(HpChangedEvent hpChangedEvent)
    {
        _enemySlider.maxValue = hpChangedEvent.MaxHp;
        _enemySlider.value = hpChangedEvent.Hp;
    }

    private void ChangePlayerExpSlider(ChangePlayerExperienceEvent changePlayerExperienceEvent)
    {
        _playerExp.maxValue = changePlayerExperienceEvent.MaxLevelExp;
        _playerExp.value = changePlayerExperienceEvent.CurrentExp;
    }
}
