using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform cameraPos;
    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
