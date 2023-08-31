using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class AttackStateOneShooter : State
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
        var bullet = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + _y, transform.position.z), Quaternion.identity);
        Vector3 shootDirection = _target.position - bullet.transform.position;
        bullet.Shot(shootDirection * _forceAttack);

        if (_animator != null)
            _animator.SetTrigger("Attack");
    }
}
