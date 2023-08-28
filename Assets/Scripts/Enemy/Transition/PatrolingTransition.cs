using UnityEngine;
using Source.EnemyView;
using System.Collections;

public class PatrolingTransition : Transition
{
    [SerializeField] private FieldOfVision _fieldOfVision;

    private Coroutine _coroutine;

    private void Update()
    {
        if (_fieldOfVision.TryFindVisibleTarget(out Player player) == false)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(Transit(5));
        }
            
    }

    private IEnumerator Transit(int time)
    {
        yield return new WaitForSeconds(time);

        NeedTransit = true;
    }
}   