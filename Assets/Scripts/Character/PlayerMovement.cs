using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _speed = 10;
    [SerializeField] private Animator _animator;

    private NavMeshAgent _agent;
    private float _speedRate;
    private Coroutine _changeSpeed;
    
    public event UnityAction PositionUpdated;
    
    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _speedRate = 1;
    }

    private void Update()
    {
        var shift = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
        _agent.SetDestination(transform.position + shift.normalized);

        _agent.speed = _speed * _speedRate;

        if (shift.magnitude == 0)
        {
            _animator.SetFloat(Speed, 0);
        }
        else if(shift.magnitude >= 0.1f)
        {
            _animator.SetFloat(Speed, 1f);
            PositionUpdated?.Invoke();
        }
    }

    public void ChangeSpeed(float speedFactor, float duration)
    {
        if (_changeSpeed != null)
            StopCoroutine(_changeSpeed);

        _changeSpeed = StartCoroutine(ChangeSpeedCoroutine(speedFactor, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float speedFactor, float duration)
    {
        _speedRate = speedFactor;
        yield return new WaitForSeconds(duration);
        _speedRate = 1;
    }
}
