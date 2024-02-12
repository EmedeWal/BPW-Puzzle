using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private Transform movingDoor;

    [SerializeField] private float maxDistance;
    [SerializeField] private float moveSpeed;

    private float moveAmount = 0.1f;

   

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Open door is called");
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        float distance = 0;

        while (distance <= maxDistance)
        {
            Debug.Log("One loop of the function");

            Vector3 newPosition = movingDoor.position + Vector3.up * moveAmount;
            movingDoor.position = newPosition;

            distance += moveAmount;

            yield return new WaitForSeconds(moveSpeed);
        }
    }
}
