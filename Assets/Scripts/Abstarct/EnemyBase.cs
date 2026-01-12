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
    public float health;
    public int damage;
    public bool attacking = false;
    public float timer = 0;
    public float attackTimer;
    [Header("Navmesh")]
    public NavMeshAgent agent;
    public GameObject player; 
    [Header("Line of Sight")]
    public LayerMask obstructionMask;
    private void Start()
    {
        obstructionMask = LayerMask.GetMask("Default");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = acceleration;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        float currentDist = Vector3.Distance(transform.position, player.transform.position);
        bool canSeePlayer = HasLineOfSight();

        if (currentDist < stopRadius && canSeePlayer)
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
           
            agent.stoppingDistance = stopRadius;
            agent.SetDestination(player.transform.position);
        }

        if (attacking)
        {
            Attack();
        }
    }
    public abstract void Attack();

    bool HasLineOfSight()
    {
        Vector3 origin = transform.position + Vector3.up * 1.5f; // eye height
        Vector3 target = player.transform.position + Vector3.up * 1.5f;

        Vector3 dir = target - origin;
        float dist = dir.magnitude;

        if (Physics.Raycast(origin, dir.normalized, out RaycastHit hit, dist, obstructionMask))
        {
            // Something blocked the ray
            return false;
        }

        return true;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            EnemyDie();
        }

    }

    void EnemyDie()
    {
        Destroy(gameObject);
    }
}
