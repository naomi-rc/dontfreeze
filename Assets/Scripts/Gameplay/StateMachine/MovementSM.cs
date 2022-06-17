using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSM : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Walk walkingState;

    private void Awake()
    {
        idleState = new Idle(this);
        walkingState = new Walk(this);
    }

    protected override State GetInitialState()
    {
        return idleState;
    }
}
