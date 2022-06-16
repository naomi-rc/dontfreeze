using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : MonoBehaviour
{
    
    [SerializeField]  float targetSpeed = 0f;


    UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();        
    }

    public void Pursue(GameObject target)
    {
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
        Vector3 predictedPosition = target.transform.position + dp;
        Vector3 d = predictedPosition - transform.position;
        agent.SetDestination(transform.position + d);
    }

    public void Evade(GameObject target)
    {
        Vector3 distance = target.transform.position - transform.position;
        Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
        Vector3 predictedPosition = target.transform.position + dp;
        agent.SetDestination(transform.position + (transform.position - predictedPosition));
    }
}
