using Project.Data;
using Project.Managers.Enemy;
using Project.Systems.StateMachine.Enemy;
using System.Xml.Serialization;
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
    [SerializeField] private Animator _enemyAnimator;


    private FSMEnemy _FSMEnemy;
    private Transform _centerPosition;
    private float _patrolRadius = 5f;
    private bool _isDead = false;
    private EnemyManager _enemyManager;

    public void Init(EnemyManager enemyManager)
    {
        _FSMEnemy = new FSMEnemy();
        _enemyManager = enemyManager;
        _FSMEnemy.Init(enemyManager, _enemyAnimator);
    }

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

        //MoveToRandomePosition();

        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    public void EnemyUpdate()
    {
        _FSMEnemy.RunOnUpdate();
        if (_currentHp <= 0 && !_isDead)
        {
            EnemyDie();
        }

        //if (!_isDead && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        //    MoveToRandomePosition();
    }

    public void EnemyOnFixesUpdate()
    {
        _FSMEnemy.RunOnFixedUpdate();
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

    public void GetDamage(int damage) 
    {
        _currentHp -= damage;
        _hpSlider.value = _currentHp;
        _FSMEnemy.FSM.ChangeState(_FSMEnemy.TakeDamageState);
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
