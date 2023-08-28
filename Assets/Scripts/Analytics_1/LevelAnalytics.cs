using UnityEngine;
using System.Collections.Generic;

public class LevelAnalytics : MonoBehaviour
{
    [SerializeField] private EndLevelTrigger _endLevelTrigger;
    [SerializeField] private LoseCanvas _loseCanvas;
    [SerializeField] private Tutorial _tutorial;

    private Analytics _analytics;
    private int _levelComplete;
    private int _levelNumber;
    private float _startTime;

    private void OnEnable()
    {
        _tutorial.Completed += OnTutorCompleted;
        _endLevelTrigger.Won += OnLevelCompleted;
        _endLevelTrigger.Lost += OnLevelFailed;
        _loseCanvas.Restarted += OnReloading;
        _analytics.EventSent += OnSentEvent;
    }

    private void OnDisable()
    {
        _tutorial.Completed -= OnTutorCompleted;
        _endLevelTrigger.Won -= OnLevelCompleted;
        _endLevelTrigger.Lost -= OnLevelFailed;
        _loseCanvas.Restarted -= OnReloading;
        _analytics.EventSent -= OnSentEvent;
    }

    private void Awake()
    {
        _analytics = Singleton<Analytics>.Instance;
        _levelNumber = Singleton<LevelLoader>.Instance.LevelIndex + 1;
        _levelComplete = Singleton<LevelLoader>.Instance.LevelCounter + 1;
    }

    private void Start()
    {
        _analytics.FireEvent("main_menu");
    }

    private void OnTutorCompleted()
    {
        var parameters = new Dictionary<string, object>() { { "level", _levelNumber }, };
        _analytics.FireEvent("level_start", parameters);
        _analytics.ForceSendEventBuffer();

        _startTime = Time.realtimeSinceStartup;
    }

    private void OnLevelFailed()
    {
        var timeSpent = Time.realtimeSinceStartup - _startTime;
        var parameters = new Dictionary<string, object>() {
            { "level", _levelNumber },
            { "time_spent", (int)timeSpent },
        };

        _analytics.FireEvent("level_fail", parameters);
    }

    private void OnLevelCompleted()
    {
        var timeSpent = Time.realtimeSinceStartup - _startTime;
        var parameters = new Dictionary<string, object>() {
            { "level", _levelNumber },
            { "time_spent", (int)timeSpent },
        };

        _analytics.FireEvent("level_complete", parameters);
        _analytics.ForceSendEventBuffer();
    }

    private void OnReloading()
    {
        var parameters = new Dictionary<string, object>() {
            { "level", _levelNumber },
        };

        _analytics.FireEvent("level_restart", parameters);
    }

    private void OnSentEvent()
    {
        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomNumber("current_soft").WithValue(_levelComplete));

        AppMetrica.Instance.SetUserProfileID(new DuckyID().Value());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }
}
