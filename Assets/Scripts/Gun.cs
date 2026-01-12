using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public int range = 100;
    public int maxAmmo = 15;
    public int ammo = 15;

    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Reload();
    }

    void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0)
        {
            ammo--;
            Debug.Log(ammo);
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~0, QueryTriggerInteraction.Ignore))
            {
                Debug.Log(hit.transform.name);

                DestroyableBox destroyableBox = hit.transform.GetComponent<DestroyableBox>();
                if (destroyableBox != null)
                {
                    destroyableBox.TakeDamage(damage);
                }

                EnemyBase enemyBase = hit.transform.GetComponent<EnemyBase>();
                if (enemyBase != null)
                {
                    enemyBase.TakeDamage(damage);
                }
            }
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ammo = maxAmmo;
            Debug.Log("Reloaded: " + ammo);
        }
    }
}
