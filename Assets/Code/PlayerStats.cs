using NUnit.Framework;
using System.Collections.Generic;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            health = 0;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "JumpPad")
        {
            uiScript.objectivesText.text = "Objective: " + uiScript.objectives[1];
            Debug.Log("Touched 1 " + other.name);
        }
        else if (other.gameObject.name == "JumpPad (1)")
        {
            uiScript.objectivesText.text = "Objective: " + uiScript.objectives[3];
            Debug.Log("Touched 2 " + other.name);
        }
        else if (other.gameObject.name == "JumpPad (2)")
        {
            uiScript.objectivesText.text = "Objective: " + uiScript.objectives[5];
            Debug.Log("Touched 3 " + other.name);
        }
        else if (other.gameObject.name == "JumpPad (3)")
        {
            uiScript.objectivesText.text = "Objective: " + uiScript.objectives[6];
            Debug.Log("Touched 2 " + other.name);
        }
        
    }

}
