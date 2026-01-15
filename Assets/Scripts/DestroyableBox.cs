using UnityEngine;

public class DestroyableBox : MonoBehaviour
{

    public float health = 1f;
    public UIScript uiScript;


    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f)
        {
            Break();
        }
    }

    void Break()
    {
        gameObject.SetActive(false);
    }

}
