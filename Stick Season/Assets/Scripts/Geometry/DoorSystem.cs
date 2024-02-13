using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour
{
    private float offset = 10f;
    private float moveDelay = 0.01f;
    private float moveAmount = 0.1f;

    private Coroutine currentCoroutine;

    private Vector3 defaultPos;
    private Vector3 newPos;

    private void Awake()
    {
        // Set the defaultPos to the current position of the gameObject in the scene. 
        defaultPos = base.transform.position;
        Debug.Log(defaultPos);
    }

    private void Start()
    {
        // Set the newPos equal to defaultPos + the offset on the yAxis.
        newPos = defaultPos + new Vector3(0, offset, 0);
        Debug.Log(newPos);
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Open door is called");

            // Check if there is a current Coroutine. If there is one active, stop it.
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);

            // Start the correct Coroutine and assign it to currentCoroutine.
            currentCoroutine = StartCoroutine(OpenDoor());
        }

       if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Close the door.");

            // Check if there is a current Coroutine. If there is one active, stop it.
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);

            // Start the correct Coroutine and assign it to currentCoroutine.
            currentCoroutine = StartCoroutine(CloseDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        // While the gameObject is lower than the desired position, move it up.
        while (transform.position.y < newPos.y)
        {
            // Move the door upwards by the increment of moveAmount.
            Vector3 newPosition = transform.position + Vector3.up * moveAmount;
            transform.position = newPosition;

            // Wait seconds equal to the moveDelay. Then, repeat.
            yield return new WaitForSeconds(moveDelay);
        }
    }

    private IEnumerator CloseDoor()
    {
        // While the gameObject is higher than the desired position, move it down.
        while (transform.position.y > defaultPos.y)
        {
            // Move the door downwards by the increment of moveAmount.
            Vector3 newPosition = transform.position + Vector3.down * moveAmount;
            transform.position = newPosition;

            // Wait seconds equal to the moveDelay. Then, repeat.
            yield return new WaitForSeconds(moveDelay);
        }
    }
}
