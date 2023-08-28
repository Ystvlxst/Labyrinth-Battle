using System.Collections;
using UnityEngine;

public class GrassCutterBooster : MonoBehaviour
{
    [SerializeField] private float _increase;
    [SerializeField] private Animator _animator;

    private const string _interaction = "Interaction";

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out GrassCutter grassCutter))
        {
            StartCoroutine(Interact());
            grassCutter.Increase(_increase);
        }
    }

    private IEnumerator Interact()
    {
        _animator.SetTrigger(_interaction);
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
