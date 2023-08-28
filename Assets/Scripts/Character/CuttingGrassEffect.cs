using UnityEngine;

public class CuttingGrassEffect : MonoBehaviour
{
    [SerializeField] private PictureEvents _levelEvents;
    [SerializeField] private ParticleSystem _cuttingEffectTemplate;
    [SerializeField] private Transform _effectContainer;

    private void OnEnable()
    {
        _levelEvents.CuttingGrass += OnCuttingGrass;
    }

    private void OnDisable()
    {
        _levelEvents.CuttingGrass -= OnCuttingGrass;
    }

    private void OnCuttingGrass()
    {
        Instantiate(_cuttingEffectTemplate, _effectContainer);
    }
}
