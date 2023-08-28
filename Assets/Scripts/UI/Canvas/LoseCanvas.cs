using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoseCanvas : CanvasWindow
{
    [SerializeField] private Button _reloadButton;

    public event UnityAction Restarted;

    private void OnEnable()
    {
        _reloadButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnDisable()
    {
        _reloadButton.onClick.RemoveListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()
    {
        Restarted?.Invoke();
        Singleton<LevelLoader>.Instance.ReloadCurrentLevel();
    }
}
