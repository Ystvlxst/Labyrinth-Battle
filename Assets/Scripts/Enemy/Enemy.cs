using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    
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
}