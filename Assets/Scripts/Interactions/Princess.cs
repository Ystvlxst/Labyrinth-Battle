using UnityEngine;

public class Princess : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] StarCounter _flowerCounter;
    [SerializeField] ParticleSystem _confetti;
    [SerializeField] ParticleSystem _hearts;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.Win();
            Reaction(_flowerCounter.CurrentStarsCount.ToString());
        }
    }

    private void Reaction(string flowersCount)
    {
        _animator.SetTrigger(flowersCount);
        _confetti.Play();
        CheckCountFlowers();
    }

    private void CheckCountFlowers()
    {
        if (_flowerCounter.CurrentStarsCount == 3)
            _hearts.Play();
        else
            return;
    }
}
