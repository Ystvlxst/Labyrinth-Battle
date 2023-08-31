using UnityEngine;
using UnityEngine.AI;

public class AttackStateTwoShooter : State
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _forceAttack;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private PatrollingState _patrollingState;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _y;

    private float _timeToShoot;

    private void OnEnable()
    {
        _patrollingState.SetSpeed(0);
    }

    private void OnDisable()
    {
        _patrollingState.SetSpeed(_patrollingState.StartSpeed);
    }

    private void Update()
    {
        transform.LookAt(_target.position);

        if (_timeToShoot <= 0)
        {
            Attack();
            _timeToShoot = _shootDelay;
        }

        _timeToShoot -= Time.deltaTime;
    }

    private void Attack()
    {
        var bullet1 = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);
        var bullet2 = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);

        Vector3 shootDirection1 = new Vector3(_target.position.x - Random.Range(1, 10), _target.position.y, _target.position.z) - bullet1.transform.position;
        Vector3 shootDirection2 = new Vector3(_target.position.x + Random.Range(-1, -10), _target.position.y, _target.position.z) - bullet2.transform.position;

        bullet1.Shot(shootDirection1 * _forceAttack);
        bullet2.Shot(shootDirection2 * _forceAttack);

        if (_animator != null)
            _animator.SetTrigger("Attack");
    }
}
