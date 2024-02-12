using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private string tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            Debug.Log("Player is standing on the pressure plate.");
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
