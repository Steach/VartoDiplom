using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    private int _maxHp = 100;
    private int _currentHp;
    private int _exp = 10;
    public int HP 
    {
        get 
        {
            return _currentHp; 
        }
        set 
        {
            if (value > 0 && _currentHp > value)
                _currentHp -= value;
            else if (value > 0 && _currentHp <= value)
                _currentHp = 0;
        }
    }

    private void Start()
    {
        _currentHp = _maxHp;
        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    private void Update()
    {
        if (_currentHp <= 0)
            EnemyDie();
    }

    private void EnemyDie()
    {
        EventBus.Publish(new EnemyDieEvent(_exp));
    }

    private void WhenHpChanged()
    {
        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            WhenHpChanged();
        }
    }
}
