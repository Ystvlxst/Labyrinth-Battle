using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthSlider;

    private void Awake()
    {
        _healthSlider.maxValue = _health.Value;
        _healthSlider.value = _health.Value;
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthCanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthCanged;
    }

    private void OnHealthCanged()
    {
        _healthSlider.value = _health.Value;
    }
}
