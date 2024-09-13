using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private float _delayBeforeUpdating = 0f;
    [SerializeField] private State _defaultState;

    private Coroutine _coroutineTick;

    public State CurrentState { get; private set; }

    private void Awake()
    {
        SetState(_defaultState);
        _coroutineTick = StartCoroutine(Tick());
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (_coroutineTick != null)
            StopCoroutine(_coroutineTick);
    }

    public void Transit(State state)
    {
        SetState(state);
    }

    private void SetState(State state)
    {
        if (CurrentState == state)
            return;

        if (CurrentState != null)
            CurrentState.Exit();

        CurrentState = state;

        if (CurrentState != null)
            CurrentState.Enter();
    }

    private IEnumerator Tick()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_delayBeforeUpdating);

            State nextState = CurrentState.GetNextState();

            if (nextState != null)
                SetState(nextState);

            CurrentState?.Tick();
        }
    }
}
