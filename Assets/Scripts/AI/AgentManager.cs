using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    [SerializeField] GameObject target;

    public GameObject GetTarget()
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
