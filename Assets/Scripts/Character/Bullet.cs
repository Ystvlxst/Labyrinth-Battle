using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;

    public float Damage => _damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
