using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float targetSpeed = 1f;
    [SerializeField] float TargetMaxDistance = 15f;
    [SerializeField] float TargetMinDistance = 2f;
    [SerializeField] int LifeForce = 15;
    [SerializeField] int MaxLifeForce = 15;

    BehaviorTree tree;
    UnityEngine.AI.NavMeshAgent agent;
    GameObject target;

    public enum ActionState { MOVING, IDLE, ROAM, ATTACK, DEAD };
    ActionState state = ActionState.IDLE;

    Node.Status treeStatus = Node.Status.Running;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        tree = new BehaviorTree();

        Selector s1 = new Selector("s1");
        tree.AddChild(s1);

        Sequence s2 = new Sequence("s2");
        Leaf die = new Leaf("Die", Die);
        s1.AddChild(s2);
        s1.AddChild(die);

        Leaf hasLife = new Leaf("HasLife", HasLife);
        Selector s3 = new Selector("s3");
        s2.AddChild(hasLife);
        s2.AddChild(s3);

        Sequence s4 = new Sequence("s4");
        Selector s5 = new Selector("s5");
        s3.AddChild(s4);
        s3.AddChild(s5);

        Leaf targetCloseby = new Leaf("TargetCloseby", TargetCloseby);
        Selector s6 = new Selector("s6");
        s4.AddChild(targetCloseby);
        s4.AddChild(s6);

        Sequence s7 = new Sequence("s7");
        Leaf evade = new Leaf("Evade", Evade);
        s6.AddChild(s7);
        s6.AddChild(evade);

        Leaf isStrong = new Leaf("IsStrong", IsStrong);
        Selector s8 = new Selector("s8");
        s7.AddChild(isStrong);
        s7.AddChild(s8);

        Sequence s9 = new Sequence("s9");
        Leaf pursue = new Leaf("Pursue", Pursue);
        s8.AddChild(s9);
        s8.AddChild(pursue);

        Leaf targetAttained = new Leaf("TargetAttained", TargetAttained);
        Leaf attack = new Leaf("Attack", Attack);
        s9.AddChild(targetAttained);
        s9.AddChild(attack);


        Leaf idle = new Leaf("Idle", Idle);
        Leaf roam = new Leaf("Roam", Roam);
        s5.AddChild(roam);
        s5.AddChild(idle);
        
        


        
        /*sequence.AddChild(pursue);
        sequence.AddChild(attack);
        sequence.AddChild(die);
        tree.AddChild(sequence);*/
        
    }

    private void Update()
    {
        //if(treeStatus == Node.Status.Running)
        treeStatus = tree.Process();
    }

    public void UpdateTarget(GameObject t)
    {
        target = t;
    }

    Node.Status GoToPosition(Vector3 destination)
    {
        float distanceToDestination = Vector3.Distance(destination, transform.position); 
        agent.SetDestination(destination);
        state = ActionState.MOVING;
        
        if(Vector3.Distance(agent.pathEndPosition, destination) >= TargetMinDistance)
        {
            state = ActionState.IDLE; //or roam
            return Node.Status.Failure;
        }
        else if(distanceToDestination < 2)
        {
            state = ActionState.IDLE; // or roam
            return Node.Status.Success;
        }
        return Node.Status.Running;
    }

    //Actions
    public Node.Status Pursue()
    {
        Debug.Log("Imma get you!! Come here!");
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
        Vector3 predictedPosition = target.transform.position + dp;
        Vector3 d = predictedPosition - transform.position;
        //return GoToPosition(transform.position + d);
        agent.SetDestination(transform.position + d);
        return Node.Status.Running;
    }

    public Node.Status Evade()
    {
        Debug.Log("Ahh! No, I don't have the strength for this!");
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
        Vector3 predictedPosition = target.transform.position + dp;
        //return GoToPosition(transform.position + (transform.position - predictedPosition));
        agent.SetDestination(transform.position + (transform.position - predictedPosition));
        return Node.Status.Running;
    }

    public Node.Status Idle()
    {
        Debug.Log("Imma stay put");
        return Node.Status.Success;
    }

    public Node.Status Roam()
    {
        Debug.Log("Ahh, what a beautiful day for a stroll");
        //30% chance of failing
        return Node.Status.Success;
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



    //Conditions
    public Node.Status HasLife()
    {
        if(LifeForce > 0)
            return Node.Status.Success;
        return Node.Status.Failure;
    }

    public Node.Status TargetCloseby()
    {
        if(agent.hasPath && Vector3.Distance(agent.pathEndPosition, agent.destination) >= TargetMinDistance)
        {
            agent.isStopped = true;

        }
        else if(target && Vector3.Distance(target.transform.position, transform.position) <= TargetMaxDistance)
        {
            return Node.Status.Success;
        }            
        return Node.Status.Failure;
    }


    public Node.Status IsStrong()
    {
        if (LifeForce/MaxLifeForce > 0.1)
            return Node.Status.Success;
        return Node.Status.Failure;
    }

    public Node.Status TargetAttained()
    {
        if (target && Vector3.Distance(target.transform.position, transform.position) <= TargetMinDistance)
            return Node.Status.Success;
        return Node.Status.Failure;
    }

}
