using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    [SerializeField] GameObject target;

    void Start()
    {
        UpdateEnemyTarget();
    }

    public GameObject getTarget()
    {
        return target;
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public void UpdateEnemyTarget()
    {
        agents = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject agent in agents)
        {
            agent.GetComponent<EnemyBehavior>().UpdateTarget(target);
        }
    }
}
