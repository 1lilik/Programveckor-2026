using UnityEngine;

public class DashPowerUp : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
