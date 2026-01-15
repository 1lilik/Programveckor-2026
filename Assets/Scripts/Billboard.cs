using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position; 
        cameraPosition.y = transform.position.y; 
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f);
    }
}
