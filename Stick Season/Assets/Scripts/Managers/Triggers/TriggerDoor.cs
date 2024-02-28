using UnityEngine;
using UnityEngine.Events;

public class TriggerDoor : MonoBehaviour
{
    public UnityEvent triedDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triedDoor?.Invoke();
        }

        Destroy(gameObject);
    }
}
