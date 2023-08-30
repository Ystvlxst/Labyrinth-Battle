using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const int WallLayerNumber = 10;

    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _wallHitEffect;

    public float Damage => _damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == WallLayerNumber)
        {
            Instantiate(_wallHitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Shot(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }
}
