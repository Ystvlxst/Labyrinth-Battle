using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthSlider;

    private void OnEnable()
    {
        _healthSlider.maxValue = _health.Value;

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
