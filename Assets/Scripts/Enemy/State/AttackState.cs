using System;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class AttackState : State
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _forceAttack;
    [SerializeField] private float _shootDelay;
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private PatrollingState _patrollingState;

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
        var bullet = Instantiate(_bulletTemplate, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
        Vector3 shootDirection = _target.position - bullet.transform.position;
        bullet.Shot(shootDirection * _forceAttack);
    }
}
