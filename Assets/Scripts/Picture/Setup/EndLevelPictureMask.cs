using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class EndLevelPictureMask : MonoBehaviour
{
    [SerializeField] private EndLevelTrigger _endTrigger;
    [SerializeField] private Texture2D _texture;
    
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        _endTrigger.Won += OnLevelComplete;
    }

    private void OnDisable()
    {
        _endTrigger.Won -= OnLevelComplete;
    }

    private void Start()
    {
        _renderer.enabled = false;
        _renderer.material.mainTexture = _texture;
    }

    private void OnLevelComplete()
    {
        _renderer.enabled = true;
    }
}
