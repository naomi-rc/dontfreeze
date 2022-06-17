using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{

    private bool input;

    public Walk(MovementSM stateMachine) : base("Walk", stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        // TODO activer l'idle animation
    }
    public override void Update()
    {
        base.Update();
        // TODO vérifier si le joueur est en mouvement, si non faire la transition vers le idle state
    }
    public override void Exit()
    {
        base.Exit();
    }
}
