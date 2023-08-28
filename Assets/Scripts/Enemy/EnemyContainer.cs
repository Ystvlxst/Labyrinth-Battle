using UnityEngine;

public class EnemyContainer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private EndLevelTrigger _endLevelTrigger;
    
    private Enemy[] _enemies;
    private SignalizationTransition[] _signalizationTransitions;
    
    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>();
        _signalizationTransitions = GetComponentsInChildren<SignalizationTransition>();

        foreach (Enemy enemy in _enemies) 
            enemy.Init(_player);
    }

    private void OnEnable()
    {
        _endLevelTrigger.Won += DisableEnemies;
        _endLevelTrigger.Lost += DisableEnemies;
    }
    
    private void OnDisable()
    {
        _endLevelTrigger.Won -= DisableEnemies;
        _endLevelTrigger.Lost -= DisableEnemies;
    }

    public void Signal()
    {
        foreach (SignalizationTransition transition in _signalizationTransitions) 
            transition.Signal();
    }

    private void DisableEnemies()
    {
        foreach (Enemy enemy in _enemies) 
            enemy.Disable();
    }
}
