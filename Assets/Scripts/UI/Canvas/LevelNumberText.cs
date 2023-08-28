using TMPro;
using UnityEngine;

public class LevelNumberText : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Won += OnLevelChanged;

        OnLevelChanged();
    }

    private void OnDisable()
    {
        _player.Won -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        int counter = Singleton<LevelLoader>.Instance.LevelCounter;
        int index = 1 + counter;
        _levelNumber.text = "Level " + index;
    }
}
