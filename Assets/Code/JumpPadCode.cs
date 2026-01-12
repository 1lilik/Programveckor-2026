using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("JumpPad Settings")]
    public Transform targetPosition;

    public float arcHeight = 5f;

    public float cooldown = 0.5f;

    private bool onCooldown;

    private void OnCollisionEnter(Collision collision)
    {
        if (onCooldown) return;
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        if (targetPosition == null)
        {
            Debug.LogError("JumpPad: Target Position is not assigned.");
            return;
        }

        Launch(rb);
    }

    void Launch(Rigidbody rb)
    {
        onCooldown = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 launchVelocity = CalculateLaunchVelocity(
            rb.position,
            targetPosition.position,
            arcHeight
        );

        rb.AddForce(launchVelocity, ForceMode.VelocityChange);

        Invoke(nameof(ResetCooldown), cooldown);
    }

    void ResetCooldown()
    {
        onCooldown = false;
    }

    Vector3 CalculateLaunchVelocity(Vector3 start, Vector3 end, float arcHeight)
    {
        float gravity = Physics.gravity.y;

        Vector3 displacement = end - start;
        Vector3 displacementXZ = new Vector3(displacement.x, 0f, displacement.z);

        float horizontalDistance = displacementXZ.magnitude;

        float time = Mathf.Max(0.1f, horizontalDistance / 10f);

        float minVerticalTime = Mathf.Sqrt(-2f * arcHeight / gravity) * 2f;
        time = Mathf.Max(time, minVerticalTime);

        Vector3 velocityXZ = displacementXZ / time;

        float velocityY =
            (displacement.y - 0.5f * gravity * time * time) / time;

        return velocityXZ + Vector3.up * velocityY;
    }
}
