using UnityEngine;
using UnityEngine.AI;
using Source.EnemyView;

[RequireComponent(typeof(NavMeshAgent))]
public class DistanceToPlayerTransition : Transition
{
    [SerializeField] private FieldOfVision _fieldOfVision;

    private void Update()
    {
        if (_fieldOfVision.TryFindVisibleTarget(out Player player))
            NeedTransit = true;
    }
}