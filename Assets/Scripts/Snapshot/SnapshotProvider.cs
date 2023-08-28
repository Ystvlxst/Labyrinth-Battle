using UnityEngine;

public class SnapshotProvider : MonoBehaviour
{
    [SerializeField] private Camera _renderCamera;
    [SerializeField] private Transform _pivot;

    private void OnValidate()
    {
        if (_pivot)
            transform.position = _pivot.position + Vector3.up * transform.position.y;
    }

    public Texture2D MakeSnapshot()
    {
        var renderTexture = _renderCamera.targetTexture;
        var texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

        var copy = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, texture2D.width, texture2D.height), 0, 0);
        texture2D.Apply();
        RenderTexture.active = copy;

        return texture2D;
    }
}
