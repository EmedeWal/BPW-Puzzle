using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Pressure Plate Variables")]
    [SerializeField] private LayerMask heavyEnough;

    [Header("Flame Pillar Reference")]
    [SerializeField] private FlamePillar flamePillar;

    [HideInInspector] public bool isTriggered;

    #region BoxCast Variables
    private Vector3 boxSize = new Vector3(5, 1, 5);
    private float timeSinceLastCast;
    private float castFrequency = 5;
    #endregion

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
                flamePillar.LightFlame();
            }
            else
            {
                isTriggered = false;
                flamePillar.ExtinguishFlame();
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
