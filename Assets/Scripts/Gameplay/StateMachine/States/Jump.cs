using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State
{
    private Vector2 movement;

    public Jump(MovementSM stateMachine) : base("Jump", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        stateMachine.inputReader.JumpEvent += ApplyJump;
        stateMachine.inputReader.MoveEvent += CheckMovement;
        stateMachine.animator.SetTrigger("jump");
        // TODO activer l'animation du jump
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
        else
        {
            stateMachine.ChangeState(((MovementSM)stateMachine).idleState);
        }
        // TODO vérifier les conditions pour passer à la prochaine animation probablement idle
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void ApplyJump()
    {
        // Faire la logique pour le jump ici
    }
    private void CheckMovement(Vector2 value)
    {
        movement = value;
    }

}
