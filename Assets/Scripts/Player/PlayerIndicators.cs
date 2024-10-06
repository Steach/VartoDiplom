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
        Debug.Log($"{_maxHP}, {_maxMP}, {_maxST}");
        Debug.Log($"{_currentHP}, {_currentMP}, {_currentST}");
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

    private void RecoveryIndicators()
    {
        if (_currentHP < _maxHP)
        {
            _currentHP += 0.1f;
            EventBus.Publish<GetDamagePlayerEvent>(new GetDamagePlayerEvent(_currentHP, _maxHP));
        }


        if (_currentMP < _maxMP)
            _currentMP += 0.1f;

        if (_currentST < _maxST)
            _currentST += 0.1f;
    }
}
