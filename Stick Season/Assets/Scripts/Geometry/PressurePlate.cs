using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private GameObject door;

    private DoorSystem ds;

    private void Awake()
    {
        ds = door.GetComponent<DoorSystem>();
    }

    private void Start()
    {
        // Set the required amount of progress for the door to +1. 
        ds.SetRequired(1);
    }

    // If the pressure is triggered, increment the progress.
    private void OnTriggerEnter(Collider other)
    {
        if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("Entered pressure plate.");
            ds.ProgressTracker(1);
        }
    }

    // If the weight from the pressure plate is removed, remove progress.
    private void OnTriggerExit(Collider other)
    {
        if (collisionLayer == (collisionLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("Exit pressure plate.");
            ds.ProgressTracker(-1);
        }
    }
}
