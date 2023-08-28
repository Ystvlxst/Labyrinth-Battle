using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private static readonly int Win = Animator.StringToHash("Win");
    private static readonly int Lose = Animator.StringToHash("Lose");
    
    [SerializeField] private Animator _animator;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Died += OnDied;
        _player.Won += OnWon;
    }
    
    private void OnDisable()
    {
        _player.Died += OnDied;
        _player.Won += OnWon;
    }

    private void OnWon()
    {
        _animator.SetTrigger(Win);
    }

    private void OnDied()
    {
        _animator.SetTrigger(Lose);
    }
}
