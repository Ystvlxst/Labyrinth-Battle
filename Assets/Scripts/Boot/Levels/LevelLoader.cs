using UnityEngine;

public class LevelLoader : Singleton<LevelLoader>
{
    private const string SavedLevelKey = nameof(SavedLevelKey);
    private const string CounterLevelKey = nameof(CounterLevelKey);

    [SerializeField] private LevelList _levelList;
    [SerializeField] private LevelLoadingScreen _loadingScreen;

    private System.Random _random = new System.Random();

    private int _maxLevelCount = 9;

    private int _savedLevel
    {
        get => PlayerPrefs.GetInt(SavedLevelKey, 0);
        set => PlayerPrefs.SetInt(SavedLevelKey, value);
    }

    private int _levelCounter
    {
        get => PlayerPrefs.GetInt(CounterLevelKey, 0);
        set => PlayerPrefs.SetInt(CounterLevelKey, value);
    }

    public int LevelIndex => _savedLevel;
    public int LevelCounter => _levelCounter;
    public int MaxLevelsCount => _maxLevelCount;

    private void OnDisable()
    {
        PlayerPrefs.SetInt(CounterLevelKey, _levelCounter);
        PlayerPrefs.SetInt(SavedLevelKey, _savedLevel);
    }

    public void LoadSavedLevel()
    {
        LoadScene(_levelList.Levels[_savedLevel].ScenePath);
    }

    public void LoadNextLevel()
    {
        _savedLevel = (_savedLevel + 1) % _levelList.Levels.Count;
        LoadScene(_levelList.Levels[_savedLevel].ScenePath);
        ChangeCounter();
    }

    public void ReloadCurrentLevel()
    {
        LoadScene(_levelList.Levels[_savedLevel].ScenePath);
    }

    public void LoadRandomLevel()
    {
        int random = _random.Next(2, _maxLevelCount);
        LoadScene(random.ToString());
        ChangeCounter();
    }

    private void LoadScene(string sceneName)
    {
        var loadingScreen = Instantiate(_loadingScreen);
        loadingScreen.LoadScene(sceneName, () => Destroy(loadingScreen.gameObject));
    }

    private void ChangeCounter()
    {
        _levelCounter++;
        PlayerPrefs.GetInt(CounterLevelKey, 0);
        PlayerPrefs.GetInt(SavedLevelKey, 0);
    }
}
