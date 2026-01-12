using System.Xml.Serialization;
using UnityEngine;

public class HealthbarTEST : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;


    //ta med i playerscript, kopplar till UI script
    public UIScript uiScript;

    
    void Start()
    {
        currentHealth = maxHealth;
        //ta med i playerscript, kopplar till UI script
        uiScript.SetMaxHealth(maxHealth);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //ta med i playerscript, kopplar till UI script
        uiScript.SetHealth(currentHealth);
    }
}
