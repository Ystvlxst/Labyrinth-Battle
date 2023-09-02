using UnityEngine;
using UnityEngine.AI;

public class AttackStateThreeShooter : State
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
        else
        {
            if (_animator != null)
                _animator.SetTrigger("Run");
        }

        _timeToShoot -= Time.deltaTime;
    }

    private void Attack()
    {
        var bullet1 = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);
        var bullet2 = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);
        var bullet3 = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);
        var bullet4 = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);

        Vector3 shootDirection1 = new Vector3(_target.position.x - 2.5f, _target.position.y + 2, _target.position.z) - bullet1.transform.position;
        Vector3 shootDirection2 = new Vector3(_target.position.x + 2.5f, _target.position.y + 2, _target.position.z) - bullet2.transform.position;
        Vector3 shootDirection3 = new Vector3(_target.position.x - 7, _target.position.y + 2, _target.position.z) - bullet1.transform.position;
        Vector3 shootDirection4 = new Vector3(_target.position.x + 7, _target.position.y + 2, _target.position.z) - bullet2.transform.position;

        bullet1.Shot(shootDirection1 * _forceAttack);
        bullet2.Shot(shootDirection2 * _forceAttack);
        bullet3.Shot(shootDirection3 * _forceAttack);
        bullet4.Shot(shootDirection4 * _forceAttack);

        if (_animator != null)
            _animator.SetTrigger("Attack");
    }
}
