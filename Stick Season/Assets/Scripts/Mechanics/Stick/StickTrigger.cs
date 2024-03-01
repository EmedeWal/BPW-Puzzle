using UnityEngine;

public class StickTrigger : MonoBehaviour
{
    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.stickToDestroy = gameObject;
            playerController.inRangeOfStick = true;
            playerController.prompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.inRangeOfStick = false;
            playerController.prompt.SetActive(false);
        }
    }
}