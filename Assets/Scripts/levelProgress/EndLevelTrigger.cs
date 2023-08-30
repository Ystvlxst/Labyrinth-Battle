using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private const string OpenDoor = "OpenDoor";

    [SerializeField] private Health[] _enemies;
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private ParticleSystem _openDoorEffect;

    private int _aliveEnemies;

    private void OnEnable()
    {
        _aliveEnemies = _enemies.Length;

        foreach (var enemy in _enemies)
            enemy.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
            enemy.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        _aliveEnemies--;

        if (_aliveEnemies <= 0)
            EndLevel();
    }

    private void EndLevel()
    {
        _doorAnimator.SetTrigger(OpenDoor);
        _openDoorEffect.Play();
    }
}
