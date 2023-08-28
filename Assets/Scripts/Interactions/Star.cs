using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Star : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _poofEffect;

    private CapsuleCollider _collider;

    private const string _interaction = "Interaction";

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out StarCounter starCounter))
        {
            StartCoroutine(Interact());
            starCounter.FindStar();
            _poofEffect.Play();
        }
    }

    private IEnumerator Interact()
    {
        _animator.SetTrigger(_interaction);
        _collider.enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        _animator.gameObject.SetActive(false);
    }
}
