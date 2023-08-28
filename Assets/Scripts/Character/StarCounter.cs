using System;
using UnityEngine;

public class StarCounter : MonoBehaviour
{
    private int _requireStarsCount;
    private int _currentStarsCount;

    public event Action CountChanged;

    public int CurrentStarsCount => _currentStarsCount;
    public int RequireStarsCount => _requireStarsCount;

    private void Start()
    {
        _requireStarsCount = 3;
        _currentStarsCount = 0;

        CountChanged?.Invoke();
    }

    public void FindStar()
    {
        _currentStarsCount++;
        CountChanged?.Invoke();
    }
}
