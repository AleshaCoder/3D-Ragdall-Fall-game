using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions = new List<Transition>();

    private void Awake()
    {
        enabled = false;
    }

    public void Enter()
    {
        Debug.Log($"Enter state {this}");
        OnEnter();
        enabled = true;
    }

    public void Exit()
    {
        Debug.Log($"Exit state {this}");
        OnExit();
        enabled = false;
    }

    public void Tick()
    {
        OnTick();
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.EntryConditions.All(condition => condition.CanEntry()))
                return transition.To;
        }

        return null;
    }

    protected abstract void OnTick();
    protected abstract void OnEnter();
    protected abstract void OnExit();
}
