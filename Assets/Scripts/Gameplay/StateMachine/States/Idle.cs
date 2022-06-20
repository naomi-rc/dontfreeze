using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Idle : State
{
    private Vector2 movement;
    private bool jump;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) { }
    
    public override void Enter() 
    {
        base.Enter();
        stateMachine.inputReader.MoveEvent += CheckMovement;
        //stateMachine.inputReader.JumpEvent += ApplyJump;
        stateMachine.animator.SetBool("isMoving", false);
    }

    // TODO Implémenter ces méthodes
    //public virtual void HandleInput() { }

    //public virtual void LogicUpdate() { }

    //public virtual void PhysicsUpdate() { }
    public override void Update() 
    { 
        base.Update();
        if (movement.sqrMagnitude > Mathf.Epsilon)
        {
            stateMachine.ChangeState(((MovementSM)stateMachine).walkingState);     
        }
    }
    public override void Exit() 
    { 
        base.Exit();
    }

    private void CheckMovement(Vector2 value)
    {
        movement = value;
    }
}
