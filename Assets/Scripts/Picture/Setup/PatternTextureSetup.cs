using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class PatternTextureSetup : MonoBehaviour
{
    [SerializeField] private Texture2D _pattern;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.mainTexture = _pattern;
    }
}
