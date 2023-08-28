using UnityEngine;
using UnityEngine.Events;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private Player _player;

    public event UnityAction Won;
    public event UnityAction Lost;

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _player.Won += OnPlayerWon;
        
        Won += Disable;
        Lost += Disable;
    }

    private void OnDisable()
    {
        _player.Died -= OnPlayerDied;
        _player.Won -= OnPlayerWon;

        Won -= Disable;
        Lost -= Disable;
    }

    private void OnPlayerDied()
    {
        Lost?.Invoke();
    }

    private void OnPlayerWon()
    {
        Won?.Invoke();
    }

    private void Disable()
    {
        enabled = false;
    }
}
