using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action Died;
    public event Action Won;

    public bool Dead { get; set; }
    
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
