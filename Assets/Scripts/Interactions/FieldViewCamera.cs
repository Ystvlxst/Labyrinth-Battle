using System.Collections;
using UnityEngine;
using Cinemachine;

public class FieldViewCamera : MonoBehaviour
{
    [SerializeField] private float _runningFieldView;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private float _idleFieldView;
    private bool _isRunning;

    private void Awake()
    {
        _idleFieldView = 60f;
        _isRunning = false;
    }

    private void Update()
    {
        CheckRunningEffect();
    }

    public void IsRunning(bool isRunning)
    {
        _isRunning = isRunning;
    }

    private void CheckRunningEffect()
    {
        if(_isRunning)
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_idleFieldView, _runningFieldView, 1);
        else
            _camera.m_Lens.FieldOfView = Mathf.Lerp(_runningFieldView, _idleFieldView, 1);
    }
}
