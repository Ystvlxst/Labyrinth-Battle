using UnityEngine;
using TMPro;

public class PlayerAccuracyView : MonoBehaviour
{
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private EndLevelTrigger _endLevelTrigger;
    [SerializeField] private TMP_Text _percentText;

    private void OnEnable()
    {
        _endLevelTrigger.Won += CheckPercentAccuracy;
    }

    private void OnDisable()
    {
        _endLevelTrigger.Won -= CheckPercentAccuracy;
    }

    private void CheckPercentAccuracy()
    {
        var accuracyPercent = ((1f - _levelProgress.LosePercentage) * 100).ToString("0.00") + "% Accuracy";
        _percentText.text = accuracyPercent;
    }
}
