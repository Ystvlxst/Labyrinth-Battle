using System;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _dieEffect;

    public float Value => _health;
    public bool IsDied { get; private set; } = false;

    public event Action HealthChanged;
    public event Action Died;

    public void TakeDamage(float value = 1)
    {
        _health -= value;

        Instantiate(_hitEffect, new Vector3(transform.position.x, transform.position.y + 1.25f,
            transform.position.z), Quaternion.identity);

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }

        HealthChanged?.Invoke();
    }

    public void Die()
    {
        Instantiate(_dieEffect, new Vector3(transform.position.x, transform.position.y + 1.25f,
            transform.position.z), Quaternion.identity);

        IsDied = true;
        _agent.enabled = false;
        Died?.Invoke();

        gameObject.SetActive(false);
    }
}
