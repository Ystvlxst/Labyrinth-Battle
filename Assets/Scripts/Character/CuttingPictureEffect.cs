using UnityEngine;
using DG.Tweening;

public class CuttingPictureEffect : MonoBehaviour
{
    [SerializeField] private PictureEvents _puctureEvents;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _model;
    [SerializeField] private Transform _smokeEffectContainer;
    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _targetColor;

    private Color _startColor;
    private Material _material;

    private void OnEnable()
    {
        _puctureEvents.CuttingPicture += OnCutPicture;
    }

    private void OnDisable()
    {
        _puctureEvents.CuttingPicture -= OnCutPicture;
        _material.color = _startColor;
    }

    private void Start()
    {
        _material = _renderer.sharedMaterial;
        _startColor = _material.color;
    }

    private void OnCutPicture()
    {
        var duration = 0.05f;

        _model.transform.localScale = Vector3.one;
        _model.DOShakeScale(duration, 0.25f, 10);
        _playerMovement.ChangeSpeed(0.2f, duration);

        ChangeColor(_targetColor, duration * 10, _startColor, duration * 10);
        var inst = Instantiate(_smokeEffect, _smokeEffectContainer);
        inst.transform.localPosition = Vector3.zero;
    }

    private void ChangeColor(Color targetColor, float durationToTarget, Color normalColor, float durationToNormal)
    {
        _material.DOKill();
        _material.DOColor(targetColor, durationToTarget)
            .OnComplete(() => _material.DOColor(normalColor, durationToNormal));
    }
}
