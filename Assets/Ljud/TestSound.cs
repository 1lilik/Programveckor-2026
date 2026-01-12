using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)){
            SoundManager.PlaySound(SoundType.PLACEHOLDER1);
        }
    }
}
