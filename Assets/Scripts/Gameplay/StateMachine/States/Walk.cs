using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{

    private Vector2 movement;
    private bool jump;

    public Walk(MovementSM stateMachine) : base("Walk", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        stateMachine.inputReader.MoveEvent += ApplyMovement;
        //stateMachine.inputReader.JumpEvent += ApplyJump;
        stateMachine.animator.SetTrigger("walk");
        // TODO activer l'idle animation
    }

    // TODO Implémenter ces méthodes pour une meilleure gestion des input et des updates
    //public virtual void HandleInput() { }

    //public virtual void LogicUpdate() { }

    //public virtual void PhysicsUpdate() { }
    public override void Update()
    {
        base.Update();
        if (movement.sqrMagnitude < Mathf.Epsilon)
        {
            stateMachine.ChangeState(((MovementSM)stateMachine).idleState);
        }
     }
    public override void Exit()
    {
        base.Exit();
    }
    private void ApplyMovement(Vector2 value)
    {
        movement = value;
    }
}
