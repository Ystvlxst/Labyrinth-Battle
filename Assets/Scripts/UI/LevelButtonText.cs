using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LevelButtonText : MonoBehaviour
{
    [SerializeField] private LevelButton _levelButton;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

        _text.text = "Level " + _levelButton.SceneName;
    }
}
