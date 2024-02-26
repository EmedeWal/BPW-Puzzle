using UnityEngine;

public class StickLogic : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 1f;

    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the stick area");
            playerController = other.GetComponent<PlayerController>();
            playerController.inRangeOfStick = true;
            playerController.stickToDestroy = gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has left the stick area");
            playerController = other.GetComponent<PlayerController>();
            playerController.inRangeOfStick = false;
        }
    }
}
