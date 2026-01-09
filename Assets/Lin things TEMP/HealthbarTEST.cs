using System.Xml.Serialization;
using UnityEngine;

public class HealthbarTEST : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;


    //ta med i playerscript, kopplar till healthbar script
    public HealthBar healthBar;

    
    void Start()
    {
        currentHealth = maxHealth;
        //ta med i playerscript, kopplar till healthbar script
        healthBar.SetMaxHealth(maxHealth);
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
        //ta med i playerscript, kopplar till healthbar script
        healthBar.SetHealth(currentHealth);
    }
}
