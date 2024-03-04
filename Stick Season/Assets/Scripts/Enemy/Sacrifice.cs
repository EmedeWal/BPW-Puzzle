using UnityEngine;

public class Sacrifice : MonoBehaviour
{
    [SerializeField] private TrapSystem trapSystem;
    [SerializeField] private Vector3 boxSize;

    private void OnDestroy()
    {
        // Collect layers of all colliders detected within the box
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2f, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            PressurePlate plate = collider.GetComponent<PressurePlate>();

            if (plate != null)
            {
                if (plate.weightType == "blood")
                {
                    plate.ActivatePlate();

                    trapSystem.ResetTrap();
                }
            }
        }
    }
}
