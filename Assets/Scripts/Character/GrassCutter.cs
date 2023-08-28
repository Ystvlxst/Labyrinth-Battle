using System.Collections;
using UnityEngine;

public class GrassCutter : MonoBehaviour
{
    private const float TrailDeactivationDelay = 1f;
    [SerializeField] private TrailRenderer _trailRendererTemplate;
    [SerializeField] private TrailRenderer _currentTrailRenderer;

    private Vector3 _scale;
    
    public void Increase(float increase)
    {
        _scale = _currentTrailRenderer.transform.localScale * increase;
        Transform parent = _currentTrailRenderer.transform.parent;
        StartCoroutine(ResetParentDelayed(_currentTrailRenderer));
        CreateNewTrail(parent);
    }

    private void CreateNewTrail(Transform parent)
    {
        _currentTrailRenderer = Instantiate(_trailRendererTemplate, parent);
        _currentTrailRenderer.transform.localScale = _scale;
    }

    private IEnumerator ResetParentDelayed(TrailRenderer currentTrailRenderer)
    {
        yield return new WaitForSeconds(TrailDeactivationDelay);
        currentTrailRenderer.transform.SetParent(null);
    }
}
