using UnityEngine;

public class StickLogic : MonoBehaviour
{

    private PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the stick area");
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
            Debug.Log("Player has left the stick area");
            playerController = other.GetComponent<PlayerController>();
            playerController.inRangeOfStick = false;
            playerController.prompt.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (playerController.prompt.activeSelf)
        {
            playerController.prompt.SetActive(false);
        }
    }
}
