using System.Threading;
using UnityEngine;

public class EnemyHitBoxRelay : MonoBehaviour
{
    private EnemyBase parentEnemy;

    private void Start()
    {
        parentEnemy = GetComponentInParent<EnemyBase>();
    }

    private void OnTriggerEnter(Collider other)
    { 

        if (other.gameObject.tag == ("Player"))
        {
            parentEnemy.timer = 0;
            parentEnemy.attacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == ("Player"))
        {
            parentEnemy.attacking = false;
        }
    }
}
