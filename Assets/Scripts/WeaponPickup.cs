using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player"))
        {
            SoundManager.PlaySound(SoundType.PLACEHOLDER4);
            gameObject.SetActive(false);
        }
    }
}
