using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("VictoryScene");
    }
}
