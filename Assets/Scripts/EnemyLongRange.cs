using System.Collections;
using UnityEngine;

public class EnemyLongRange : EnemyBase
{
    [Header("ProjectileSettings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Animator animator;

    public override void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            animator.SetTrigger("Shoot");
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
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
