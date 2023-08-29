using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFreezeState : State
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    private float _startSpeed;

    public event Action Froze;

    private void Awake()
    {
        if (_navMeshAgent.enabled == true)
            _startSpeed = _navMeshAgent.speed;
    }

    public void OnEnable()
    {
        if (_navMeshAgent.enabled == true)
            _navMeshAgent.speed = 0;
        
        Froze?.Invoke();
    }

    private void OnDisable()
    {
        StartCoroutine(FreezeDelay());
    }

    private IEnumerator FreezeDelay()
    {
        yield return new WaitForSeconds(2);
        if (_navMeshAgent.enabled == true)
            _navMeshAgent.speed = _startSpeed;
    }
}