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

}
