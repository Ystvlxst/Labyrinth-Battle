using UnityEngine;

public class GrassBendingTrailPosition : MonoBehaviour
{
    [SerializeField] private InstancedIndirectGrassPosDefine _instancedIndirectGrass;

    private void Update()
    {
        var position = transform.position;
        position.y = _instancedIndirectGrass.transform.position.y;
        transform.position = position;
    }
}
