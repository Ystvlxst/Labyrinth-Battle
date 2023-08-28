using UnityEngine;
using TMPro;

public class StarCountText : MonoBehaviour
{
    [SerializeField] private StarCounter _starCounter;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _starCounter.CountChanged += OnCountChanged;
    }

    private void OnDisable()
    {
        _starCounter.CountChanged -= OnCountChanged;
    }

    private void OnCountChanged()
    {
        _text.text = _starCounter.CurrentStarsCount.ToString() + "/" + _starCounter.RequireStarsCount;
    }
}
