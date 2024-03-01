using UnityEngine;

public class StickTrigger : MonoBehaviour
{
    private PlayerController playerController;
    private EnemyAI enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            playerController.stickToDestroy = gameObject;
            playerController.inRangeOfStick = true;
            playerController.prompt.SetActive(true);
        }

        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<EnemyAI>();

            if (enemy != null) 
            {
                enemy.Trip();
            }
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