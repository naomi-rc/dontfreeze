using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : State
{

    [SerializeField] private IntVariable playerHealth; // TODO à utiliser pour vérifier la santé du joueur


    public Death(MovementSM stateMachine) : base("Death", stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        // TODO activer l'animation de la mort
    }

    // TODO Implémenter ces méthodes
    //public virtual void HandleInput() { }

    //public virtual void LogicUpdate() { }

    //public virtual void PhysicsUpdate() { }
    public override void Update()
    {
        base.Update();
        // TODO vérifier les conditions pour passer à la prochaine animation
    }
    public override void Exit()
    {
        base.Exit();
    }
   
}
