using Source.EnemyView;
using UnityEngine;

public class AttackTransition : Transition
{
    [SerializeField] private FieldOfVision _fieldOfVision;
    [SerializeField] private PatrolingTransition _patroolingTransition;

    private void Update()
    {
        if (_fieldOfVision.TryFindVisibleTarget(out Player player))
            NeedTransit = true;
        else
        {
            NeedTransit = false;
            _patroolingTransition.Transit();
        }
    }
}
