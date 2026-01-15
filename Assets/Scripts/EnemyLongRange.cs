using UnityEngine;

public class EnemyLongRange : EnemyBase
{
    [Header("ProjectileSettings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    public override void Attack()
    {
       
        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            SoundManager.PlaySound(SoundType.PLACEHOLDER3);
            Vector3 direction = (player.transform.position - transform.position).normalized;

         
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed; 
            }
            timer = 0;
        }
 
    }
}
