using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthView : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private Slider _healthSlider;

    private void OnEnable()
    {
        _healthSlider.maxValue = _enemyHealth.Value;

        _enemyHealth.HealthChanged += OnHealthCanged;
    }

    private void OnDisable()
    {
        _enemyHealth.HealthChanged -= OnHealthCanged;
    }

    private void OnHealthCanged()
    {
        _healthSlider.value = _enemyHealth.Value;
    }
}
