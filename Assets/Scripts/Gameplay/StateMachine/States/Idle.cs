using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool input;

    public Idle(MovementSM stateMachine) : base("Idle", stateMachine) { }
    public override void Enter() 
    {
        base.Enter();
        // TODO activer l'idle animation
    }
    public override void Update() 
    { 
        base.Update();
        // TODO vérifier si le joueur est en mouvement, si oui faire la transition vers le walking state
    }
    public override void Exit() 
    { 
        base.Exit();
    }
}
