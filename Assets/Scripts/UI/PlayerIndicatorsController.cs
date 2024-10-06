using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicatorsController : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _playerIndicators;
    [SerializeField] private Image _playerHPIndicator;
    [SerializeField] private Image _playerMPIndicator;
    [SerializeField] private Image _playerSTIndicator;
    [SerializeField] private Image _playerExpIndicator;
    [SerializeField] private TextMeshProUGUI _playerLevelText;

    //public GameObject PlayerIndocatorsContainer { get { return _playerIndicators; } }

    private void OnEnable()
    {
        EventBus.Subscribe<GetDamagePlayerEvent>(ChangeHpIndicator);
        EventBus.Subscribe<ChangeMPIndicatorEvent>(ChangeMPIndicator);
        EventBus.Subscribe<ChangeStaminaImdicatorEvent>(ChangeSTIndicator);
        EventBus.Subscribe<LevelUpEvent>(ChangeLevelIndicator);
    }

    private void Start()
    {
        FirstSetPlayerIndicators();
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GetDamagePlayerEvent>(ChangeHpIndicator);
        EventBus.Unsubscribe<ChangeMPIndicatorEvent>(ChangeMPIndicator);
        EventBus.Unsubscribe<ChangeStaminaImdicatorEvent>(ChangeSTIndicator);
        EventBus.Unsubscribe<LevelUpEvent>(ChangeLevelIndicator);
    }

    private void FirstSetPlayerIndicators()
    {
        var playerIndicators = _uiManager.GameManager.PlayerManager.PlayerIndicators;
        SetNormalizedIndicator(playerIndicators.CurrentHP, playerIndicators.MaxHP, _playerHPIndicator);
        SetNormalizedIndicator(playerIndicators.CurrentMP, playerIndicators.MaxMP, _playerMPIndicator);
        SetNormalizedIndicator(playerIndicators.CurrentST, playerIndicators.MaxST, _playerSTIndicator);
    }

    private void ChangeHpIndicator(GetDamagePlayerEvent getDamagePlayerEvent)
    {
        SetNormalizedIndicator(getDamagePlayerEvent.CurrentHP, getDamagePlayerEvent.MaxHP, _playerHPIndicator);
    }

    private void ChangeMPIndicator(ChangeMPIndicatorEvent changeMPIndicatorEvent)
    {
        SetNormalizedIndicator(changeMPIndicatorEvent.CurrentMP, changeMPIndicatorEvent.MaxMP, _playerMPIndicator);
    }

    private void ChangeSTIndicator(ChangeStaminaImdicatorEvent changeStaminaImdicatorEvent)
    {
        SetNormalizedIndicator(changeStaminaImdicatorEvent.CurrentST, changeStaminaImdicatorEvent.MaxST, _playerSTIndicator);
    }

    private void ChangeLevelIndicator(LevelUpEvent levelUpEvent)
    {
        _playerLevelText.text = levelUpEvent.NewLevel.ToString();

        SetNormalizedIndicator((float)levelUpEvent.CurrentExp, (float)levelUpEvent.NextLevelExp, _playerExpIndicator);
    }

    private void SetNormalizedIndicator(float current, float max, Image indicator)
    {
        indicator.fillAmount = current / max;
    }
}
