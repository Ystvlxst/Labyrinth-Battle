using UnityEngine;
using UnityEngine.AI;

public class AttackStateDirect : State
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _distanceToAttack;
    [SerializeField] private float _damage;
    [SerializeField] private PatrollingState _patrollingState;
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    [SerializeField] private Health _playerHealth;

    private void OnEnable()
    {
        _patrollingState.SetSpeed(6);
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Vector3.Distance(transform.position, _playerHealth.transform.position) <= _distanceToAttack)
        {
            transform.LookAt(_playerHealth.transform.position);
            _agent.SetDestination(_playerHealth.transform.position);

            if (Vector3.Distance(transform.position, _playerHealth.transform.position) <= 12)
                _animator.SetTrigger("Attack");

            if (Vector3.Distance(transform.position, _playerHealth.transform.position) <= 1.5f)
            {
                _playerHealth.TakeDamage(_damage);
                _health.Die();
            }
        }
    }
}
