using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private string pressureTag;
    [SerializeField] private GameObject door; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            Debug.Log("Player standing on the pressure plate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tag))
        {
            Debug.Log("Player is no longer standing on the pressure plate.");
        }
    }
}
