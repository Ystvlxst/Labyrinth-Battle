using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;
    [SerializeField] private bool _randomTransition = false;

    public event Action<State> Entered;
    public event Action<State> Exited;
    
    public void Enter()
    {
        if (enabled == false)
        { 
            enabled = true;
            
            foreach (var transition in _transitions)
                transition.enabled = true;

            Entered?.Invoke(this);
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
            
            Exited?.Invoke(this);
        }
    }

    public State GetNextState()
    {
        if (_randomTransition)
            return GetRandomTransition();

        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    private State GetRandomTransition()
    {
        var transitions = new List<Transition>();

        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                transitions.Add(transition);
        }

        if (transitions.Count == 0)
            return null;

        return transitions[Random.Range(0, transitions.Count)].TargetState;
    }
}
