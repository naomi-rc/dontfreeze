using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{

    private bool input;

    private Vector2 movement;


    public Attack(MovementSM stateMachine) : base("Attack", stateMachine) { }



    public override void Enter()
    {
        base.Enter();
        stateMachine.inputReader.MoveEvent += AttackEnemy;
        // TODO activer l'attack animation
    }
    public override void Update()
    {
        base.Update();
        // TODO vérifier les conditions pour changer d'état
        
    }

    // TODO Implémenter ces méthodes
    //public virtual void HandleInput() { }

    //public virtual void LogicUpdate() { }

    //public virtual void PhysicsUpdate() { }
    public override void Exit()
    {
        base.Exit();
    }
    private void AttackEnemy(Vector2 value)
    {
        // TODO faire la logique pour attaquer l'enemi
        movement = value;
    }
}
