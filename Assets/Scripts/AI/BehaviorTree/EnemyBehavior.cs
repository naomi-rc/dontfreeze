using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float targetSpeed = 1f;
    [SerializeField] float TargetMaxDistance = 15f;

    BehaviorTree tree;
    UnityEngine.AI.NavMeshAgent agent;
    GameObject target;

    public enum ActionState { IDLE, ROAM, DEAD, EVADE, PURSUE, ATTACK, MOVING};
    ActionState state = ActionState.IDLE;

    Node.Status treeStatus = Node.Status.Running;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        tree = new BehaviorTree();

        Sequence sequence = new Sequence();
        Leaf pursue = new Leaf("Pursue", Pursue);
        Leaf attack = new Leaf("Attack", Attack);
        Leaf die = new Leaf("Die", Die);

        sequence.AddChild(pursue);
        sequence.AddChild(attack);
        sequence.AddChild(die);
        tree.AddChild(sequence);
        
    }

    private void Update()
    {
        if(treeStatus == Node.Status.Running)
            treeStatus = tree.Process();
    }

    public void UpdateTarget(GameObject t)
    {
        target = t;
    }

    Node.Status GoToPosition(Vector3 destination)
    {
        float distanceToDestination = Vector3.Distance(destination, transform.position);
        if(state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.MOVING;
        }
        else if(Vector3.Distance(agent.pathEndPosition, destination) >= TargetMaxDistance)
        {
            state = ActionState.IDLE;
            return Node.Status.Failure;
        }
        else if(distanceToDestination < 2)
        {
            state = ActionState.IDLE;
            return Node.Status.Success;
        }
        return Node.Status.Running;
    }

    public Node.Status Pursue()
    {
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
        Vector3 predictedPosition = target.transform.position + dp;
        Vector3 d = predictedPosition - transform.position;
        return GoToPosition(transform.position + d);
    }

    public Node.Status Evade()
    {
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
        Vector3 predictedPosition = target.transform.position + dp;
        return GoToPosition(transform.position + (transform.position - predictedPosition));
    }

    public Node.Status Attack()
    {
        Debug.Log("Grrr!!");
        return Node.Status.Success;
    }

    public Node.Status Die()
    {
        Debug.Log("Tell my children.. I.. love them... *gasp*");
        return Node.Status.Success;
    }
}
