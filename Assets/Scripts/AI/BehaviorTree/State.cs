using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class State
{
    public enum Event { Enter, Update, Exit }

    protected Event stage;
    protected State nextState;

    public State()
    {
        stage = Event.Enter;
    }

    public virtual void Enter() { stage = Event.Update; }
    public virtual void Update() { stage = Event.Update; }
    public virtual void Exit() { stage = Event.Exit; }

    public State Process()
    {
        if (stage == Event.Enter) Enter();
        if (stage == Event.Update) Update();
        if (stage == Event.Exit)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
