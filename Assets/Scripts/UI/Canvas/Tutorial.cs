using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _tutorRoot;

    public event UnityAction Completed;

    public bool IsCompleted { get; private set; } = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsCompleted)
            return;
        
        _tutorRoot.SetActive(false);
        IsCompleted = true;
        Completed?.Invoke();
    }
}
