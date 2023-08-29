using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private State _firstState;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _dieEffect;

    private State _currentState;

    public bool IsDied { get; private set; } = false;
    public Player Player { get; private set; }

    public void Init(Player player)
    {
        Player = player;
    }

    public void Start()
    {
        Transit(_firstState);
    }

    public void Disable()
    {
        _currentState.Exit();
        enabled = false;
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        
        if (nextState != null)
            Transit(nextState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.Die();
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }

    public void TakeDamage(float value = 1)
    {
        _health -= value;

        Instantiate(_hitEffect, transform.position, Quaternion.identity);

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    private void Die()
    {
        Instantiate(_dieEffect, transform.position, Quaternion.identity);
        IsDied = true;
        _agent.enabled = false;
        gameObject.SetActive(false);
    }
}