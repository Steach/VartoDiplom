using Project.Data;
using UnityEngine;
using UnityEngine.UI;

public class EnemySimple : MonoBehaviour
{
    private int _maxHp = 100;
    [SerializeField] private int _currentHp;
    [SerializeField] private int _exp;
    [SerializeField] private GameObject _parent;
    [SerializeField] private Slider _hpSlider;

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

        if (_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHp;
            _hpSlider.value = _currentHp;
        }
        
        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    private void Update()
    {
        if (_currentHp <= 0)
            EnemyDie();
    }

    private void EnemyDie()
    {
        EventBus.Publish(new EnemyDieEvent(_exp, transform.position));
        _parent.SetActive(false);
    }

    public void GetDamage() 
    {
        _hpSlider.value = _currentHp;
    }

    private void WhenHpChanged()
    {
        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameData.WeaponTag))
        {
            WhenHpChanged();
        }
    }
}
