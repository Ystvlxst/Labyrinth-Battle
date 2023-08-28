using UnityEngine;

namespace Source.EnemyView
{
    public class FieldOfVision : MonoBehaviour
    {
        [SerializeField] private float _viewRadius;
        [SerializeField] [Range(0, 360)] private float _viewAngle;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstacleMask;

        public float ViewRadius => _viewRadius;
        public float ViewAngle => _viewAngle;
        public LayerMask ObstacleMask => _obstacleMask;

        public bool TryFindVisibleTarget<TTarget>(out TTarget foundedTarget) where TTarget : MonoBehaviour
        {
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, _targetMask);
            foundedTarget = null;

            foreach (var targetCollider in targetsInViewRadius)
            {
                Transform target = targetCollider.transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                dirToTarget.y = 0;

                if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                    {
                        if (target.TryGetComponent(out TTarget component))
                        {
                            foundedTarget = component;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}