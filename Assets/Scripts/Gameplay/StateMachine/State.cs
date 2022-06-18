using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public enum Event { Enter, Update, Exit }

    protected Event stage;
    protected State nextState;

    protected string name;
    protected StateMachine stateMachine;

    public State(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
        stage = Event.Enter;
    }

    public virtual void Enter() { stage = Event.Update; }

    // TODO Implémenter ces méthodes
    //public virtual void HandleInput() { }

    //public virtual void LogicUpdate() { }

    //public virtual void PhysicsUpdate() { }
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
