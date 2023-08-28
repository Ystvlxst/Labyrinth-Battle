using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class Firefly : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private float _distance;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.baseOffset = 1.5f;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _agent.destination = _player.transform.position;
        SetSpeed();
    }

    private void SetSpeed()
    {
        if (Vector3.Distance(_agent.transform.position, _player.transform.position) <= _distance)
            _agent.speed = 0;
        else
        {
            if (_player.Agent.speed == 0)
            {
                _agent.SetDestination(_player.transform.position);

                if (_agent.remainingDistance == _distance)
                    _agent.speed = 0;
            }
            else
                _agent.speed = _player.Agent.speed;
        }
    }
}
