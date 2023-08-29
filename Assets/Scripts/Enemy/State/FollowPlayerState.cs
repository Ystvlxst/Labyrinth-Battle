using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowPlayerState : State
{
    [SerializeField] private Enemy _enemy;
    private Transform _player => _enemy.Player.transform;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(_agent.enabled == true)
            _agent.SetDestination(_player.position);
    }

    private void OnDisable()
    {
        if (_agent.enabled == true)
            _agent.SetDestination(transform.position);
    }
}