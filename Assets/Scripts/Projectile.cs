using UnityEngine;

public class Projectile : MonoBehaviour
{

    EnemyBase enemybaseScript;
    PlayerStats playerStatsScript;


    private void Awake()
    {
        playerStatsScript = GetComponent<PlayerStats>();
        enemybaseScript = GetComponent<EnemyBase>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);

            playerStatsScript.TakeDamage(enemybaseScript.damage);
        }

        if (!other.CompareTag("Trigger"))
        {
            Destroy(gameObject);
        }
    }


}
