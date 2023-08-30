using UnityEngine;

public class OutputCamera : MonoBehaviour
{
    [SerializeField] private CameraBlend _cameraBlend;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            //_cameraBlend.PlayerFollow();
            player.EnableShooting();
        }
    }
}
