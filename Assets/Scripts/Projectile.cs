using UnityEngine;

public class Projectile : MonoBehaviour
{

    private EnemyBase enemybaseScript;
    private PlayerStats playerStatsScript;
    private GameObject player;


    private void Awake()
    {
        player = GameObject.Find("Player");
        SoundManager.PlaySound(SoundType.PLACEHOLDER3);
        enemybaseScript = GetComponent<EnemyBase>();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerStatsScript = player.GetComponent<PlayerStats>();
        if (other.CompareTag("Player"))
        {
            playerStatsScript.TakeDamage(10);
            Destroy(gameObject);

          
        }

        else if (!other.CompareTag("Trigger"))
        {
            Destroy(gameObject);
        }
    }


}
