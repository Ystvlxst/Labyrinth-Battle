using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FrozeTransition : Transition
{
    [SerializeField] private EnemyFreezeState enemyFreezeState;
    
    protected override void Enable()
    {
        enemyFreezeState.Froze += OnFroze;
    }

    private void OnDisable()
    {
        enemyFreezeState.Froze -= OnFroze;
    }

    private void OnFroze()
    {
        NeedTransit = true;
    }
}