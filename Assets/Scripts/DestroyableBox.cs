using UnityEngine;

public class DestroyableBox : MonoBehaviour
{

    public float health = 1f;

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
        Destroy(gameObject);
    }

}
