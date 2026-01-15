using UnityEngine;

public class DashPowerUp : MonoBehaviour
{
    public UIScript uiScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (GameObject.Find("Player"))
        {
            uiScript.objectivesText.text = "Objective" + uiScript.objectives[0];
            //gameObject.SetActive(false);
        }
    }
}
