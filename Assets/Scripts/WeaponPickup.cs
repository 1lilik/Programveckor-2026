using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
