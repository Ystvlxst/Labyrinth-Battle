using UnityEngine;

public abstract class CanvasWindow : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    public void Show()
    {
        _canvas.enabled = true;
        OnShown();
    }

    public void Hide()
    {
        _canvas.enabled = false;
        OnHide();
    }

    public virtual void OnShown() { }
    public virtual void OnHide() { }
}
