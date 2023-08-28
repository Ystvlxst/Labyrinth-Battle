using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrollingState : State
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _distanceToChangeGoal;

    private Transform[] _goals;
    private NavMeshAgent _agent;
    private int _currentGoal = 0;
    public float StartSpeed { get; private set; }
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        if (_path == null)
            throw new NullReferenceException("Null path");
        
        _goals = _path.GetComponentsInChildren<Transform>().Where(child => child != _path).ToArray();
    }

    private void OnEnable()
    {
        UpdateCurrentGoal();
    }

    private void Start()
    {
        StartSpeed = _agent.speed;
    }

    private void Update()
    {
        CheckNextGoal();
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public void CheckNextGoal()
    {
        if (_agent.remainingDistance < _distanceToChangeGoal)
        {
            _currentGoal++;

            if (_currentGoal == _goals.Length)
                _currentGoal = 0;
            
            UpdateCurrentGoal();
        }
    }

    private void UpdateCurrentGoal()
    {
        _agent.destination = _goals[_currentGoal].position;
    }
}