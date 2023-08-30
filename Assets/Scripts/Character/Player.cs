using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerShooting _playerShooting;

    public event Action Died;
    public event Action Won;

    public bool Dead { get; set; }

    private void Awake()
    {
        DisableShooting();
    }

    public void EnableShooting()
    {
        _playerShooting.enabled = true;
    }

    public void DisableShooting()
    {
        _playerShooting.enabled = false;
    }

    public void Die()
    {
        if(Dead)
            return;
        
        Dead = true;
        Died?.Invoke();
    }

    public void Win()
    {
        Won?.Invoke();
    }
}
