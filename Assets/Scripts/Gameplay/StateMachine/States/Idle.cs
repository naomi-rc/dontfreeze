using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Idle : State
{
    private bool input;
    [SerializeField]
    private InputReader inputReader;

    private Vector2 movement;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) { }
    
    
    public override void Enter() 
    {
        base.Enter();
        input = false;
        stateMachine.inputReader.MoveEvent += CheckMovement;
        // TODO activer l'idle animation
    }
    public override void Update() 
    { 
        base.Update();
        if (movement.sqrMagnitude > Mathf.Epsilon)
        {
            stateMachine.ChangeState(((MovementSM)stateMachine).walkingState);
        }
        // TODO vérifier si le joueur est en mouvement, si oui faire la transition vers le walking state
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
