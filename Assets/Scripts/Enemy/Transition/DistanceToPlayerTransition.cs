using UnityEngine;
using UnityEngine.AI;
using Source.EnemyView;

[RequireComponent(typeof(NavMeshAgent))]
public class DistanceToPlayerTransition : Transition
{
    [SerializeField] private bool _notVisible = true;
    [SerializeField] private FieldOfVision _fieldOfVision;

    private void Update()
    {
        NeedTransit = CheckDistance() ^ _notVisible;
    }

    private bool CheckDistance()
    {
        return _fieldOfVision.TryFindVisibleTarget(out Player player);
    }
}