using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase: MonoBehaviour
{
    [Header ("Movement")]
    public int speed;
    public int acceleration;
    public float stopRadius;
    [Header("Stats")]
    public bool ranged;
    public int health;
    public int damage;
    public bool attacking = false;
    public float timer = 0;
    public float attackTimer;
    [Header("Navmesh")]
    public NavMeshAgent agent;
    public GameObject player;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = acceleration;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float currentDist = Vector3.Distance(transform.position, player.transform.position);

        if (currentDist < stopRadius)
        {
            Vector3 dirToTarget = transform.position - player.transform.position;
            Vector3 retreatPos = transform.position + dirToTarget.normalized * (stopRadius - currentDist);

            if (NavMesh.SamplePosition(retreatPos, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else
        { 
            agent.SetDestination(player.transform.position);
            agent.stoppingDistance = stopRadius;
        } 

        if (attacking)
        {
            Attack();
        }
    }
    public abstract void Attack();
}
