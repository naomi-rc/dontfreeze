using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{

    private bool input;

    private Vector2 movement;


    public Walk(MovementSM stateMachine) : base("Walk", stateMachine) { }

    private void Awake()
    {
        //inputReader.EnableGameplayInput();
        //inputReader.MoveEvent += ApplyMovement;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.inputReader.MoveEvent += ApplyMovement;
        // TODO activer l'idle animation
    }
    public override void Update()
    {
        base.Update();
        if (movement.sqrMagnitude < Mathf.Epsilon)
        {
            stateMachine.ChangeState(((MovementSM)stateMachine).idleState);
        }
        // TODO vérifier si le joueur est en mouvement, si non faire la transition vers le idle state
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
