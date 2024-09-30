using Project.Data;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySimple : MonoBehaviour
{
    private int _maxHp = 100;
    [SerializeField] private int _currentHp;
    [SerializeField] private int _exp;
    [SerializeField] private GameObject _parent;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Transform[] _points;
    [SerializeField] private NavMeshAgent _agent;

    private Transform _centerPosition;
    private float _patrolRadius = 5f;
    private bool _isDead = false;

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

    public void EnemyOnEnable()
    {
        _isDead = false;
    }

    public void EnemyOnDisable()
    {
        _isDead = true;
    }

    public void EnemyStart()
    {
        _centerPosition = gameObject.transform;
        _currentHp = _maxHp;

        if (_hpSlider != null)
        {
            _hpSlider.maxValue = _maxHp;
            _hpSlider.value = _currentHp;
        }

        MoveToRandomePosition();

        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    public void EnemyUpdate()
    {
        if (_currentHp <= 0 && !_isDead)
        {
            EnemyDie();
        }

        if (!_isDead && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            MoveToRandomePosition();
    }

    private void EnemyDie()
    {
        var spawnDropPosition = new Vector3(transform.position.x, 1, transform.position.z);
        EventBus.Publish(new EnemyDieEvent(_exp, spawnDropPosition));
        _agent.isStopped = true;
        gameObject.SetActive(false);
        _isDead = true;
    }

    private void MoveToRandomePosition()
    {
        var randomDirection = Random.insideUnitSphere * _patrolRadius;
        randomDirection += _centerPosition.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, _patrolRadius, NavMesh.AllAreas))
            _agent.SetDestination(hit.position);
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
