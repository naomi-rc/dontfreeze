using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    [SerializeField] GameObject target;


    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject agent in agents)
        {            
            agent.GetComponent<EnemyBehavior>().UpdateTarget(target);
        }
    }

}
