using Project.Data;
using Project.Managers.Player;
using UnityEngine;

public class PlayerIndicators
{
    private float _maxHP;
    private float _maxMP;
    private float _maxST;
    private float _currentHP;
    private float _currentMP;
    private float _currentST;
    private float _hpRecoveryPerSecond;
    private float _mpRecoveryPerSecond;
    private float _stRecoveryPerSecond;
    private PlayerManager _playerManager;

    public float MaxHP { get { return _maxHP; } }
    public float MaxMP { get { return _maxMP; } }
    public float MaxST { get { return _maxST; } }
    public float CurrentHP { get { return _currentHP; } }
    public float CurrentMP { get { return _currentMP; } }
    public float CurrentST { get { return _currentST; } }

    public void Init(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        SetPlayerIndicators(_playerManager.PlayerLevelingSystem.STR, _playerManager.PlayerLevelingSystem.INT, _playerManager.PlayerLevelingSystem.AGL);
        SetRecoveryIndicators(_playerManager.PlayerLevelingSystem.STR, _playerManager.PlayerLevelingSystem.INT, _playerManager.PlayerLevelingSystem.AGL);
    }

    public void RunOnEnable()
    {
        EventBus.Subscribe<MeleeAttakeEvent>(UseStamina);
        EventBus.Subscribe<RangeAttakeEvent>(UseStamina);
        EventBus.Subscribe<AddStatsEvent>(RefreshRecoveryIndicators);
    }

    public void RunOnDisable()
    {
        EventBus.Unsubscribe<MeleeAttakeEvent>(UseStamina);
        EventBus.Unsubscribe<RangeAttakeEvent>(UseStamina);
        EventBus.Unsubscribe<AddStatsEvent>(RefreshRecoveryIndicators);
    }

    public void GetDamage(float damage)
    {
        if (_currentHP >= damage)
            _currentHP -= damage;
        else if (_currentHP < damage)
            _currentHP = 0;

        EventBus.Publish<GetDamagePlayerEvent>(new GetDamagePlayerEvent(_currentHP, _maxHP));
    }

    public void GetDamage(int damage)
    {
        if(_currentHP >= damage)
            _currentHP -= (float)damage;
        else if(_currentHP < damage)
            _currentHP = 0;

        EventBus.Publish<GetDamagePlayerEvent>(new GetDamagePlayerEvent(_currentHP, _maxHP));
    }

    public void RunOnUpdate()
    {
        RecoveryIndicators();
    }

    private void UseStamina(MeleeAttakeEvent meleeAttakeEvent)
    {
        if(_currentST >= meleeAttakeEvent.Stamina)
        {
            _currentST -= meleeAttakeEvent.Stamina;
            EventBus.Publish<ChangeStaminaImdicatorEvent>(new ChangeStaminaImdicatorEvent(_currentST, _maxST));
        }
    }

    private void UseStamina(RangeAttakeEvent rangeAttakeEvent)
    {
        if (_currentST >= rangeAttakeEvent.Stamina)
        {
            _currentST -= rangeAttakeEvent.Stamina;
            EventBus.Publish<ChangeStaminaImdicatorEvent>(new ChangeStaminaImdicatorEvent(_currentST, _maxST));
        }
    }

    private void RecoveryIndicators()
    {
        if (_currentHP < _maxHP)
        {
            _currentHP += _hpRecoveryPerSecond;
            EventBus.Publish<GetDamagePlayerEvent>(new GetDamagePlayerEvent(_currentHP, _maxHP));
        }


        if (_currentMP < _maxMP)
        {
            _currentMP += _mpRecoveryPerSecond;
            EventBus.Publish<ChangeMPIndicatorEvent>(new ChangeMPIndicatorEvent(_currentMP, _maxMP));
        }
            

        if (_currentST < _maxST)
        {
            _currentST += _stRecoveryPerSecond;
            EventBus.Publish<ChangeStaminaImdicatorEvent>(new ChangeStaminaImdicatorEvent(_currentST, _maxST));
        }
    }

    private void RefreshRecoveryIndicators(AddStatsEvent addStatsEvent)
    {
        SetRecoveryIndicators(addStatsEvent.NewSTR, addStatsEvent.NewINT, addStatsEvent.NewAGL);
        SetPlayerIndicators(addStatsEvent.NewSTR, addStatsEvent.NewINT, addStatsEvent.NewAGL);
    }

    private void SetRecoveryIndicators(int strStat, int intStat, int aglStat)
    {
        var baseRecovery = GameData.BaseIndicatorsRecovery;
        _hpRecoveryPerSecond = (baseRecovery * strStat) / 3;
        _mpRecoveryPerSecond = (baseRecovery * intStat) / 3;
        _stRecoveryPerSecond = (baseRecovery * strStat) / 3;
    }

    private void SetPlayerIndicators(int strStat, int intStat, int aglStat)
    {
        _currentHP = _maxHP = (GameData.StartedPlayerHP + (strStat * 10)) - 30;
        _currentMP = _maxMP = (GameData.StartedPlayerMP + (intStat * 10)) - 30;
        _currentST = _maxST = (GameData.StartedPlayerST + (strStat * 10)) - 30;
    }
}
