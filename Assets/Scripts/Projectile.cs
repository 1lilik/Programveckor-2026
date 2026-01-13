using UnityEngine;

public class Projectile : MonoBehaviour
{

    EnemyBase enemybaseScript;
    PlayerStats playerStatsScript;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Trigger"))
        {
            Destroy(gameObject);

            playerStatsScript.TakeDamage(enemybaseScript.damage);
        }
    }


}
