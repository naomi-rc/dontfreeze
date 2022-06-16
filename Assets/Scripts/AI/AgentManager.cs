using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    [SerializeField] GameObject target;
    [SerializeField] float TargetMaxDistance = 15f;


    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject agent in agents)
        {
            
            agent.GetComponent<EnemyBehavior>().UpdateTarget(target);
        }
    }

    void Update()
    {
        /*foreach(GameObject agent in agents)
        {
            if (IsCloseby(agent, target))
            {
                agent.GetComponent<EnemyAgent>().Pursue(target);
            }
        }*/
        
    }

    bool IsCloseby(GameObject agent, GameObject target)
    {
        return (target.transform.position - agent.transform.position).magnitude < TargetMaxDistance;
    }
}
