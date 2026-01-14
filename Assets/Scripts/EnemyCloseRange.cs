using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyCloseRange : EnemyBase
{
    private PlayerStats playerStats;
    public override void Attack()
    {
        playerStats = player.GetComponent<PlayerStats>();
        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            playerStats.TakeDamage(10);
            timer = 0; 
        }

    }
}
