using UnityEngine;

public class HP : MonoBehaviour
{
    public float hp; 

    public void TakeDmg(int dmg)
    {
        hp -= dmg; 
    }
}
