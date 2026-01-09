using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyCloseRange : EnemyBase
{
   
    public override void Attack()
    {

        timer += Time.deltaTime;
        if (timer >= attackTimer)
        {
            Debug.Log("Attack"); //gör spelaren tar skada
            timer = 0; 
        }

    }
}
