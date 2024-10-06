using UnityEngine;

public class PlayerIndicators
{
    private float _maxHP;
    private float _maxMP;
    private float _maxST;
    private float _currentHP;
    private float _currentMP;
    private float _currentST;

    public float MaxHP { get { return _maxHP; } }
    public float MaxMP { get { return _maxMP; } }
    public float MaxST { get { return _maxST; } }
    public float CurrentHP { get { return _currentHP; } }
    public float CurrentMP { get { return _currentMP; } }
    public float CurrentST { get { return _currentST; } }

    public void Init(float maxHP, float maxMP, float maxST)
    {
        _currentHP = _maxHP = maxHP;
        _currentMP = _maxMP = maxMP;
        _currentST = _maxST = maxST;
    }

    public void RunOnEnable()
    {
        EventBus.Subscribe<MeleeAttakeEvent>(UseStamina);
        EventBus.Subscribe<RangeAttakeEvent>(UseStamina);
    }

    public void RunOnDisable()
    {
        EventBus.Unsubscribe<MeleeAttakeEvent>(UseStamina);
        EventBus.Unsubscribe<RangeAttakeEvent>(UseStamina);
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
            _currentHP += 0.03f;
            EventBus.Publish<GetDamagePlayerEvent>(new GetDamagePlayerEvent(_currentHP, _maxHP));
        }


        if (_currentMP < _maxMP)
        {
            _currentMP += 0.03f;
            EventBus.Publish<ChangeMPIndicatorEvent>(new ChangeMPIndicatorEvent(_currentMP, _maxMP));
        }
            

        if (_currentST < _maxST)
        {
            _currentST += 0.03f;
            EventBus.Publish<ChangeStaminaImdicatorEvent>(new ChangeStaminaImdicatorEvent(_currentST, _maxST));
        }
    }
}
