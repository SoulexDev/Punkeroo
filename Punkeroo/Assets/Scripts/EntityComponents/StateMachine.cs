using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine<T> : MonoBehaviour where T : StateMachine<T>
{
    //public bool serverAuthoritative = false;
    public Dictionary<Enum, State<T>> stateDictionary = new Dictionary<Enum, State<T>>();
    public State<T> currentState;
    private bool m_switchingState;

    protected delegate void StateSwitched();
    protected event StateSwitched OnStateSwitched;
    public Enum previousState;
    public Enum nextState;

    public virtual void Update()
    {
        if (m_switchingState || currentState == null)
            return;

        currentState.UpdateState((T)this);
    }
    public virtual void FixedUpdate()
    {
        if (m_switchingState || currentState == null)
            return;

        currentState.FixedUpdateState((T)this);
    }
    private void SwitchState(State<T> state)
    {
        if (currentState == state && !state.canEnterSelf || m_switchingState)
        {
            return;
        }

        m_switchingState = true;

        //currentState?.UpdateState((T)this);
        currentState?.ExitState((T)this);

        currentState = state;

        currentState.EnterState((T)this);

        m_switchingState = false;
    }
    public void SwitchState(Enum state)
    {
        nextState = state;
        previousState = stateDictionary.FirstOrDefault(k => k.Value == currentState).Key;
        SwitchState(stateDictionary[state]);

        OnStateSwitched?.Invoke();
    }
    public bool SwitchByCondition(Enum state, bool condition)
    {
        if (condition)
        {
            SwitchState(state);
            return true;
        }
        return false;
    }
}