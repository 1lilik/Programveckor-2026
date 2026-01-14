using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public int range = 100;
    public int maxAmmo = 15;
    public int ammo = 15;

    public Camera fpsCam;

    public Animator animator;

    bool haveGun = false;
    public WeaponPickup weaponPickupScript;
    public UIScript uiScript;


    // Update is called once per frame
    void Update()
    {
        ActivateWeapon();
        Shoot();
        Reload();
    }

    void Shoot()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0 && haveGun == true)
        {
            animator.SetTrigger("Shoot");
            ammo--;
            Debug.Log(ammo);
            RaycastHit hit;
            SoundManager.PlaySound(SoundType.PLACEHOLDER1);
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
        if (Input.GetKeyDown(KeyCode.R) && ammo != 15 && haveGun == true)
        {
            ammo = maxAmmo;
            Debug.Log("Reloaded: " + ammo);
        }
    }

    void ActivateWeapon()
    {
        if (!weaponPickupScript.isActiveAndEnabled)
        {
            haveGun = true;
            uiScript.ActivateWeapon();
            Debug.Log(haveGun);
        }
    }
}
