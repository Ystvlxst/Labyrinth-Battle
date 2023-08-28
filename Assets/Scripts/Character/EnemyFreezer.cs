using System.Collections;
using Source.EnemyView;
using UnityEngine;

public class EnemyFreezer : MonoBehaviour
{
    [SerializeField] private FieldOfVision _fieldOfVision;
    [SerializeField] private float _delayBetweenFreeze = 0.1f;

    private void Start()
    {
        StartCoroutine(Freeze());
    }

    private IEnumerator Freeze()
    {
        while (true)
        {
            if (_fieldOfVision.TryFindVisibleTarget(out FreezeTransition freezeTransition))
            {
                if(Vector3.Dot(freezeTransition.transform.forward, transform.forward) < 0)
                    freezeTransition.Transit();
            }
            
            yield return new WaitForSeconds(_delayBetweenFreeze);
        }
    }
}