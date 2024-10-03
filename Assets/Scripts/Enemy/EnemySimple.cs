using Project.Data;
using Project.Managers.Enemy;
using Project.Systems.StateMachine.Enemy;
using System.Collections;
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
    [SerializeField] private float _detectRadius;
    [SerializeField] private GameObject _target;


    private FSMEnemy _FSMEnemy;
    private Transform _centerPosition;
    private float _patrolRadius = 5f;
    private bool _isDead = false;
    private EnemyManager _enemyManager;
    private Collider _enemyCollider;
    private float _patrolTimerMax = 5;
    private float _patrolTimer = 0;
    //private float _stuckTimer = 0;
    //private float _stuckTimerMax = 1f;

    public GameObject Target { get { return _target; } }

    public void Init(EnemyManager enemyManager)
    {
        _FSMEnemy = new FSMEnemy();
        _enemyManager = enemyManager;
        _FSMEnemy.Init(enemyManager, _enemyAnimator, _agent, this);

        if (TryGetComponent<Collider>(out Collider collider))
            _enemyCollider = collider;
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

        EventBus.Publish(new HpChangedEvent(_currentHp, _maxHp));
    }

    public void EnemyUpdate()
    {
        Detect();
        _FSMEnemy.RunOnUpdate();
        if (_currentHp <= 0 && !_isDead)
        {
            EnemyDie();
        }

        if (!_isDead && !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance && Timer() == 0)
            MoveToRandomePosition();
    }

    public void EnemyOnFixesUpdate()
    {
        _FSMEnemy.RunOnFixedUpdate();
    }

    public void GetDamage(int damage)
    {
        _currentHp -= damage;
        _hpSlider.value = _currentHp;
        _FSMEnemy.FSM.ChangeState(_FSMEnemy.TakeDamageState);
    }

    private void EnemyDie()
    {
        var spawnDropPosition = new Vector3(transform.position.x, 1, transform.position.z);
        EventBus.Publish(new EnemyDieEvent(_exp, spawnDropPosition));
        _agent.isStopped = true;
        _isDead = true;
        _FSMEnemy.FSM.ChangeState(_FSMEnemy.DeathState);

        if(_enemyCollider != null)
            _enemyCollider.enabled = false;

        StartCoroutine(Die());
    }

    private void MoveToRandomePosition()
    {
        if (_target == null)
        {
            //var oldDestination = _centerPosition.position;
            var randomDirection = Random.insideUnitSphere * _patrolRadius;
            randomDirection += _centerPosition.position;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, _patrolRadius, NavMesh.AllAreas))
                _agent.SetDestination(hit.position);

            //if (_agent.velocity.sqrMagnitude < 3)
            //{
            //    _stuckTimer += Time.deltaTime;
            //    Debug.Log(_stuckTimer);
            //}
            //
            //if (_stuckTimer >= _stuckTimerMax)
            //{
            //    _agent.ResetPath();
            //    _agent.SetDestination(oldDestination);
            //}
            //
            //oldDestination = hit.position;
        }
    }

    private void Detect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Player"))
                _target = hitCollider.gameObject;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
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

    private float Timer()
    {
        if (_patrolTimer < _patrolTimerMax)
            _patrolTimer += Time.deltaTime;
        else if (_patrolTimer >= _patrolTimerMax)
            _patrolTimer = 0;

        return _patrolTimer;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
