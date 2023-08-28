#if UNITY_EDITOR
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Camera))]
public class PictureCameraSetup : MonoBehaviour
{
    [SerializeField] private InstancedIndirectGrassPosDefine _grass;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera ??= GetComponent<Camera>();

        var scale = _grass.transform.lossyScale;
        _camera.orthographicSize = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
        transform.position = _grass.transform.position + Vector3.up;
    }
}
#endif