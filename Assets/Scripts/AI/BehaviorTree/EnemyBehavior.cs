using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] float targetSpeed = 1f;
        [SerializeField] float targetMaxDistance = 15f;
        [SerializeField] float targetMinDistance = 2f;
        [SerializeField] Animator animator;

        public string attackSound;
        public string hurtSound;
        public string deathSound;

        private EnemyHealthController enemyController;
        bool canAttackAgain = true;
        BehaviorTree tree;
        GameObject target;
        UnityEngine.AI.NavMeshAgent agent;

        private Collider[] colliderZone;

        public enum ActionState { IDLE, WANDER, PURSUE, EVADE, ATTACK, DEAD };
        ActionState state = ActionState.IDLE;


        void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            enemyController = GetComponent<EnemyHealthController>();
            UpdateTarget(FindObjectOfType<AgentManager>().GetTarget());
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
            Leaf roam = new Leaf("Wander", Wander);
            s5.AddChild(idle);
            s5.AddChild(roam);

        }

        private void Update()
        {
            tree.Process();
            animator.SetInteger("state", ((int)state));
        }

        private void FixedUpdate()
        {
            colliderZone = Physics.OverlapSphere(this.transform.position, 2f);
        }

        public void UpdateTarget(GameObject t)
        {
            target = t;
        }


        /* Behaviour tree actions */
        public Node.Status Idle()
        {
            //0.5% chance of failing
            if (state == ActionState.WANDER || Random.Range(0f, 1f) * 1000 < 5)
            {
                return Node.Status.Failure;
            }
            state = ActionState.IDLE;
            return Node.Status.Running;
        }

        public Node.Status Wander()
        {
            if (state != ActionState.WANDER)
            {
                //Choose random angle to rotate by
                float angle = Random.Range(-10, 10);
                agent.transform.Rotate(0, angle * agent.speed * Time.deltaTime, 0);

                //Choose random distance to travel
                float distance = Random.Range(5, 15);
                agent.SetDestination(agent.transform.forward * distance);
                state = ActionState.WANDER;
            }
            else if (agent.remainingDistance < 0.1f)
            {
                state = ActionState.IDLE;
            }
            else if (agent.hasPath && Vector3.Distance(agent.pathEndPosition, agent.destination) >= targetMinDistance)
            {
                agent.ResetPath();

            }
            return Node.Status.Running;
        }

        public Node.Status Pursue()
        {
            state = ActionState.PURSUE;
            Vector3 distance = target.transform.position - transform.position;
            Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
            Vector3 predictedPosition = target.transform.position + dp;
            Vector3 d = predictedPosition - transform.position;
            agent.SetDestination(transform.position + d);
            return Node.Status.Running;
        }

        public Node.Status Evade()
        {
            state = ActionState.EVADE;
            Vector3 distance = target.transform.position - transform.position;
            Vector3 dp = distance.magnitude / agent.speed * target.transform.forward * targetSpeed;
            Vector3 predictedPosition = target.transform.position + dp;
            agent.SetDestination(transform.position + (transform.position - predictedPosition));
            return Node.Status.Running;
        }

        void AttackAgain()
        {
            canAttackAgain = true;
        }

        public Node.Status Attack()
        {
            state = ActionState.ATTACK;
            if (canAttackAgain)
            {
                foreach (var collider in colliderZone)
                {
                    if (collider.gameObject.TryGetComponent(out HealthController playerHealthController))
                    {
                        playerHealthController.TakeDamage();
                        FindObjectOfType<AudioManager>().Play(attackSound);
                    }
                }
                canAttackAgain = false;
                Invoke("AttackAgain", 3);
            }
            return Node.Status.Running;
        }

        public Node.Status Die()
        {
            FindObjectOfType<AudioManager>().Play(deathSound);

            state = ActionState.DEAD;
            Destroy(gameObject, 2f);
            return Node.Status.Success;
        }



        /* Behaviour tree conditions */
        public Node.Status HasLife()
        {
            if (enemyController.enemytHealth > 0)
                return Node.Status.Success;
            return Node.Status.Failure;
        }

        public Node.Status TargetCloseby()
        {
            if (agent.hasPath && Vector3.Distance(agent.pathEndPosition, agent.destination) >= targetMinDistance)
            {
                agent.isStopped = true;

            }
            else if (target && Vector3.Distance(target.transform.position, transform.position) <= targetMaxDistance)
            {
                return Node.Status.Success;
            }
            return Node.Status.Failure;
        }


        public Node.Status IsStrong()
        {
            if (enemyController.enemytHealth / enemyController.maxHealth > 0.1)
                return Node.Status.Success;
            return Node.Status.Failure;
        }

        public Node.Status TargetAttained()
        {
            if (target && Vector3.Distance(target.transform.position, transform.position) <= targetMinDistance)
                return Node.Status.Success;
            return Node.Status.Failure;
        }
    }
}