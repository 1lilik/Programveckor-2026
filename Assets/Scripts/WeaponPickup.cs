using UnityEngine;

public class WeaponPickup : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player"))
        {
            Destroy(gameObject);
        }
    }
}
