using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TimerTransition : Transition
{
    [SerializeField] private float _time;
    private Coroutine _transit;

    protected override void Enable()
    {
        if (_transit != null)
        {
            StopCoroutine(_transit);
            _transit = null;
        }
        
        _transit = StartCoroutine(Transit());
    }

    private IEnumerator Transit()
    {
        yield return new WaitForSeconds(_time);
        NeedTransit = true;
        _transit = null;
    }
}