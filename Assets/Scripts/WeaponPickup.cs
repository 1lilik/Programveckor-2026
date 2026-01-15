using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public UIScript uiScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player"))
        {
            uiScript.objectivesText.text = "Objective: " + uiScript.objectives[2];
            SoundManager.PlaySound(SoundType.PLACEHOLDER4);
            gameObject.SetActive(false);
        }
    }
}
