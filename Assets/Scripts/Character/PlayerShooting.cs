using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Bullet _bulletTamplate;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _shootFieldViewRadius;
    [SerializeField] private List<Health> _enemies;

    private float _timeToShoot;
    private Health _nearestEnemy;

    private void Update()
    {
        if (_nearestEnemy == null || _nearestEnemy.IsDied)
        {
            FindNearestEnemy();
        }

        Shoot();
    }

    private void FindNearestEnemy()
    {
        _nearestEnemy = null;
        float nearestDistance = _shootFieldViewRadius;

        foreach (var enemy in _enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < nearestDistance)
            {
                nearestDistance = distanceToEnemy;
                _nearestEnemy = enemy;
            }
        }
    }

    private void Shoot()
    {
        if (_nearestEnemy != null)
        {
            if (_nearestEnemy.IsDied)
            {
                _enemies.Remove(_nearestEnemy);
                _nearestEnemy = null;
                return;
            }

            _timeToShoot -= Time.deltaTime;

            if (_timeToShoot <= 0)
            {
                Bullet bullet = Instantiate(_bulletTamplate, new Vector3(transform.position.x, transform.position.y + 7, transform.position.z), Quaternion.identity);
                Vector3 shootDirection = _nearestEnemy.transform.position - bullet.transform.position;
                bullet.Shot(shootDirection * _shootForce);

                ResetShootTime();
            }

            transform.LookAt(_nearestEnemy.transform);
        }
    }

    private void ResetShootTime()
    {
        _timeToShoot = _shootDelay;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.1f);
        Gizmos.DrawSphere(transform.position, _shootFieldViewRadius);
    }
}
