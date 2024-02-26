using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private LayerMask heavyEnough;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private float castFrequency;

    [HideInInspector] public bool isTriggered;

    private float timeSinceLastCast;

    private void Update()
    {
        // Increment timeSinceLastCast
        timeSinceLastCast += Time.deltaTime;

        // Check if it's time to cast the box again
        if (timeSinceLastCast >= 1f / castFrequency)
        {
            // Reset timeSinceLastCast
            timeSinceLastCast = 0f;

            // Cast the box
            CastBox();
        }
    }

    private void CastBox()
    {
        // Collect layers of all colliders detected within the box
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2f, Quaternion.identity);

        // Display the layers of all detected colliders
        foreach (Collider collider in colliders)
        {
            if (heavyEnough == (heavyEnough | (1 << collider.gameObject.layer)))
            {
                isTriggered = true;
            }
            else
            {
                isTriggered = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw wireframe box gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
