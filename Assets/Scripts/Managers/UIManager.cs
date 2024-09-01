using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _enemySlider;
    [SerializeField] private Slider _playerExp;
    [SerializeField] private TextMeshProUGUI _playerLevelDebug;
    private int _score = 0;

    private void OnEnable()
    {
        EventBus.Subscribe<IncreasingScoreEvent>(IncreasScore);
        EventBus.Subscribe<HpChangedEvent>(ChangeEnemyHP);
        EventBus.Subscribe<ChangePlayerExperienceEvent>(ChangePlayerExpSlider);
        EventBus.Subscribe<LevelUpEvent>(PlayerLevelUp);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<IncreasingScoreEvent>(IncreasScore);
        EventBus.Unsubscribe<HpChangedEvent>(ChangeEnemyHP);
        EventBus.Unsubscribe<ChangePlayerExperienceEvent>(ChangePlayerExpSlider);
        EventBus.Unsubscribe<LevelUpEvent>(PlayerLevelUp);
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

    private void PlayerLevelUp(LevelUpEvent levelUpEvent)
    {
        _playerExp.maxValue = levelUpEvent.NextLevelExp;
        _playerExp.value = levelUpEvent.CurrentExp;
        _playerLevelDebug.text = levelUpEvent.NewLevel.ToString();
    }
}
