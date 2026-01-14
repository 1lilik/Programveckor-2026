using UnityEngine;

public class Projectile : MonoBehaviour
{

    private EnemyBase enemybaseScript;
    private PlayerStats playerStatsScript;
    private GameObject player;


    private void Awake()
    {
        player = GameObject.Find("Player");
        enemybaseScript = GetComponent<EnemyBase>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerStatsScript = player.GetComponent<PlayerStats>();
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);

            playerStatsScript.TakeDamage(10);
        }

        if (!other.CompareTag("Trigger"))
        {
            Destroy(gameObject);
        }
    }


}
