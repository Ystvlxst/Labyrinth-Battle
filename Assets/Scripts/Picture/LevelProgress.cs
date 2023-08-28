using UnityEngine;
using UnityEngine.Events;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playermovement;
    [SerializeField] private BlackWhiteColorsComputor _computor;

    public event UnityAction Updated;

    public float WinPercentage { get; private set; }
    public float LosePercentage { get; private set; }

    private void OnEnable()
    {
        _playermovement.PositionUpdated += UpdateProgress;
    }

    private void OnDisable()
    {
        _playermovement.PositionUpdated -= UpdateProgress;
    }

    private void UpdateProgress()
    {
        _computor.UpdateColors(() =>
        {
            var previousWinPercentage = WinPercentage;
            var previousLosePercentage = LosePercentage;

            WinPercentage = 1f - (float)_computor.Colors.BlackColors / _computor.Colors.StartBlackColors;
            LosePercentage = 1f - (float)_computor.Colors.WhiteColors / _computor.Colors.StartWhiteColors;

            if (previousWinPercentage != WinPercentage || previousLosePercentage != LosePercentage)
                Updated?.Invoke();
        });
    }
}
