using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BlackWhiteColorsComputor : MonoBehaviour
{
    [SerializeField] public Camera _renderCamera;
    [SerializeField] public ComputeShader _shader;

    private RenderTexture _inputTexture;
    private BlackWhiteColors _colors;
    private ComputeBuffer _outputBuffer;
    private NativeArray<uint> _outputData;
    private AsyncGPUReadbackRequest _request;
    private int _kernelInitialize;
    private int _kernelMain;
    private uint _threadX;
    private uint _threadY;

    public IBlackWhiteColors Colors => _colors;
    private int BlackColorIndex => 0;
    private int WhiteColorIndex => 255 * 3;

    private void Start()
    {
        _inputTexture = new RenderTexture(512, 512, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);

        _kernelInitialize = _shader.FindKernel("ColorSeparatorInitialize");
        _kernelMain = _shader.FindKernel("ColorSeparatorMain");
        _shader.GetKernelThreadGroupSizes(_kernelMain, out _threadX, out _threadY, out _);

        _outputBuffer = new ComputeBuffer(256 * 3, sizeof(uint));
        _outputData = new NativeArray<uint>(256 * 3, Allocator.Temp);

        _shader.SetTexture(_kernelMain, "InputTexture", _inputTexture);
        _shader.SetBuffer(_kernelInitialize, "OutputBuffer", _outputBuffer);
        _shader.SetBuffer(_kernelMain, "OutputBuffer", _outputBuffer);

        _renderCamera.targetTexture = _inputTexture;
        _renderCamera.Render();

        Execute(() =>
        {
            var textureSize = new Vector2Int(_inputTexture.width, _inputTexture.height);
            _colors = new BlackWhiteColors(textureSize, (int)_outputData[BlackColorIndex], (int)_outputData[WhiteColorIndex]);
        });
    }

    public void UpdateColors(Action onUpdated)
    {
        Execute(onUpdated);
    }

    private void Execute(Action onExecuted = null)
    {
        if (_request.done == false)
            return;

        _shader.Dispatch(_kernelInitialize, 256 * 3 / 64, 1, 1);
        _shader.Dispatch(_kernelMain, (int)(_inputTexture.width / _threadX), (int)(_inputTexture.height / _threadY), 1);

        _request = AsyncGPUReadback.Request(_outputBuffer, (request) =>
        {
            _outputData = request.GetData<uint>();
            _colors?.Update((int)_outputData[BlackColorIndex], (int)_outputData[WhiteColorIndex]);
            onExecuted?.Invoke();
        });
    }

    private void OnDestroy()
    {
        if (_outputBuffer != null)
            _outputBuffer.Release();
    }
}