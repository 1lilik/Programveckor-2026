using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public Transform targetPosition;
    public float arcHeight = 5f;
    public float travelTime = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Movment movement = other.GetComponent<Movment>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (!movement || !rb || !targetPosition)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(JumpPadRoutine(other.transform, rb, movement));
    }

    private System.Collections.IEnumerator JumpPadRoutine(
        Transform player,
        Rigidbody rb,
        Movment movement)
    {
        movement.canMove = false;
        SoundManager.PlaySound(SoundType.PLACEHOLDER5);
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;

        Vector3 start = player.position;
        Vector3 end = targetPosition.position;

        float time = 0f;

        while (time < travelTime)
        {
            float t = time / travelTime;

            Vector3 pos = Vector3.Lerp(start, end, t);
            pos.y += arcHeight * Mathf.Sin(Mathf.PI * t);

            player.position = pos;

            time += Time.deltaTime;
            yield return null;
        }

        player.position = end;

        rb.isKinematic = false;
        movement.canMove = true;
    }
}
