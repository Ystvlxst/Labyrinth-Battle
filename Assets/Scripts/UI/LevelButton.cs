using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LevelButton : MonoBehaviour
{
    [SerializeField] private int _levelName;
    [SerializeField] private SceneReference _sceneReference;

    private Button _button;

    public int SceneName => _levelName;

    private void OnEnable()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene(_levelName);
    }
}
