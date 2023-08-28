using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class InstancedIndirectGrassPosDefine : MonoBehaviour
{
    public int instanceCount = 1000000;
    public float drawDistance = 125;
    [Header("If Needed")]
    [SerializeField] private Texture2D _pattern;

    private int cacheCount = -1;

    void Start()
    {
        UpdatePosIfNeeded();
    }

    private void Update()
    {
        UpdatePosIfNeeded();
    }

    private void UpdatePosIfNeeded()
    {
        if (instanceCount == cacheCount)
            return;

        Debug.Log("UpdatePos (Slow)");

        if (_pattern == null)
            UpdateByScale();
        else
            UpdateByPattern();
    }

    private void UpdateByScale()
    {
        //same seed to keep grass visual the same
        UnityEngine.Random.InitState(123);

        //auto keep density the same
        //float scale = Mathf.Sqrt((instanceCount / 4)) / 2f;
        //transform.localScale = new Vector3(scale, transform.localScale.y, scale);

        //////////////////////////////////////////////////////////////////////////
        //can define any posWS in this section, random is just an example
        //////////////////////////////////////////////////////////////////////////
        List<Vector3> positions = new List<Vector3>(instanceCount);
        for (int i = 0; i < instanceCount; i++)
        {
            Vector3 pos = new Vector3(
                UnityEngine.Random.Range(-1f, 1f) * transform.lossyScale.x,
                0,
                UnityEngine.Random.Range(-1f, 1f) * transform.lossyScale.z
                );

            //transform to posWS in C#
            pos += transform.position;

            positions.Add(new Vector3(pos.x, pos.y, pos.z));
        }

        //send all posWS to renderer
        InstancedIndirectGrassRenderer.instance.allGrassPos = positions;
        cacheCount = positions.Count;
    }

    private void UpdateByPattern()
    {
        var texturePositions = new List<Vector3>();
        var pixels = _pattern.GetPixels();
        for (int y = 0; y < _pattern.height; y++)
            for (int x = 0; x < _pattern.width; x++)
                if (pixels[y * _pattern.width + x] == Color.white || pixels[y * _pattern.width + x] == Color.black)
                    texturePositions.Add(new Vector3(x, 0, y));
            
        var scale = transform.lossyScale.x / _pattern.width * 2;
        var textureCenter = new Vector3(_pattern.width / 2f, 0, _pattern.height / 2f);
        var positions = new List<Vector3>();
        for (int i = 0; i < instanceCount; i++)
        {
            var texturePos = texturePositions[Random.Range(0, texturePositions.Count)];
            var position = transform.position + (texturePos - textureCenter) * scale;
            positions.Add(position);
        }
        
        InstancedIndirectGrassRenderer.instance.allGrassPos = positions;
        cacheCount = positions.Count;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale * 2);
    }
}
