using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const int WallLayerNumber = 10;
    private const int EnemyLayerNumber = 11;
    private const int PlayerLayerNumber = 7;

    [SerializeField] private float _damage;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ParticleSystem _wallHitEffect;

    public float Damage => _damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Health enemy) && other.gameObject.layer == EnemyLayerNumber)
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == WallLayerNumber)
        {
            Instantiate(_wallHitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(other.TryGetComponent(out Health player) && other.gameObject.layer == PlayerLayerNumber)
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    public void Shot(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.Impulse);
    }
}
