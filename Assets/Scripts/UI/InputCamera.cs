using UnityEngine;

public class InputCamera : MonoBehaviour
{
    [SerializeField] private CameraBlend _cameraBlend;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            _cameraBlend.StartCamera();
        }
    }
}
