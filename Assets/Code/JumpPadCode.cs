using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("JumpPad Settings")]
    public Transform targetPosition;
    public float arcHeight = 5f;
    public float cooldown = 0.5f;

    private bool onCooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (onCooldown) return;
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (!rb) return;

        if (!targetPosition)
        {
            Debug.LogError("JumpPad: Target Position not assigned.");
            return;
        }

        Launch(rb);
    }

    private void Launch(Rigidbody rb)
    {
        onCooldown = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 velocity = CalculateLaunchVelocity(
            rb.position,
            targetPosition.position,
            arcHeight
        );

        rb.AddForce(velocity, ForceMode.VelocityChange);

        Invoke(nameof(ResetCooldown), cooldown);
    }

    private void ResetCooldown()
    {
        onCooldown = false;
    }
    Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float height)
    {
        float gravity = Physics.gravity.y; 

        float apexY = Mathf.Max(start.y, end.y) + height;

        float displacementUp = apexY - start.y;
        float displacementDown = apexY - end.y;

        float velocityY = Mathf.Sqrt(-2f * gravity * displacementUp);

        float timeUp = velocityY / -gravity;
        float timeDown = Mathf.Sqrt(2f * displacementDown / -gravity);
        float totalTime = timeUp + timeDown;

        Vector3 displacementXZ = new Vector3(
            end.x - start.x,
            0f,
            end.z - start.z
        );

        Vector3 velocityXZ = displacementXZ / totalTime;

        return velocityXZ + Vector3.up * velocityY;
    }
}
