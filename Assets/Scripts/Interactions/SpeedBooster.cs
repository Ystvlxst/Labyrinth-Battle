using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpeedBooster : MonoBehaviour
{
    private const string _interaction = "Interaction";
    
    [SerializeField] private Animator _animator;
    [SerializeField] private CameraBlend _camera;
    [SerializeField] private GameObject _light;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement playerMovement))
        {
            StartCoroutine(Interact());
            StartCoroutine(RunningEffect());
            playerMovement.ChangeSpeed(2, 10);
        }
    }

    private IEnumerator Interact()
    {
        _animator.SetTrigger(_interaction);
        yield return new WaitForSeconds(1);
        _collider.enabled = false;
        _light.SetActive(false);
    }

    private IEnumerator RunningEffect()
    {
        _camera.RunningCamera();
        yield return new WaitForSeconds(10);
        _camera.PlayerFollow();
    }
}
