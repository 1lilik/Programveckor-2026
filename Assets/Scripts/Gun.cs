using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public int range = 100;

    public Camera fpsCam;

    public LayerMask layerMask = -1;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~0, QueryTriggerInteraction.Ignore))
        {
            Debug.Log(hit.transform.name);
            
            DestroyableBox destroyableBox = hit.transform.GetComponent<DestroyableBox>();
            if(destroyableBox != null)
            {
                destroyableBox.TakeDamage(damage);
            }

            EnemyBase enemyBase = hit.transform.GetComponent<EnemyBase>();
            if(enemyBase != null)
            {
                enemyBase.TakeDamage(damage);
            }
        }
    }
}
