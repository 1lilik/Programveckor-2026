using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;
    public UIScript uiScript;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        uiScript.SetMaxHealth(health);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        uiScript.SetHealth(health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
