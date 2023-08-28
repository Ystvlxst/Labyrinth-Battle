using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelLoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Transform _loadingIndicator;

    private Coroutine _loadOperation;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName, Action onLoaded = null)
    {
        if (_loadOperation != null)
            throw new InvalidOperationException("Another scene is already loading");

        _loadOperation = StartCoroutine(LoadSceneOperation(sceneName, onLoaded));
    }

    private IEnumerator LoadSceneOperation(string sceneName, Action onLoaded)
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.DOFade(1f, 0.1f);
        var operation = SceneManager.LoadSceneAsync(sceneName);

        while (operation.isDone == false)
        {
            yield return null;
            _loadingIndicator.transform.Rotate(0, 0, 10f * Time.deltaTime);
        }
        
        _canvasGroup.DOFade(0f, 0.5f).OnComplete(() => onLoaded?.Invoke());
    }
}
