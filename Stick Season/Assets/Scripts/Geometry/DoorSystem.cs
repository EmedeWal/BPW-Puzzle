using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour
{
    // Variables to control when the door opens.
    private int required = 0;
    private int progress = 0;

    // Variables for door movement.
    private float offset = 10f;
    private float moveDelay = 0.01f;
    private float moveAmount = 0.1f;

    private Coroutine currentCoroutine;

    private Vector3 defaultPos;
    private Vector3 newPos;

    private void Awake()
    {
        // Set the defaultPos to the current position of the gameObject in the scene. 
        defaultPos = transform.position;
    }

    private void Start()
    {
        // Set the newPos equal to defaultPos + the offset on the yAxis.
        newPos = defaultPos + new Vector3(0, offset, 0);
    }

    // Functions for deciding when the door opens and closes.
    public void SetRequired(int localRequired)
    {
        // Set the required progress equal to the amount of gameObjects that hold a reference to the door.
        required += localRequired;
    }

    public void ProgressTracker(int localProgress)
    {
        // Increment the progress. 
        progress += localProgress;

        // If the progress is equal to the required amount of progress, open the door.
        if (progress == required)
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(OpenDoor());
        }
        // Else, check if the door is at its default position. If not, close it.
        else if (transform.position.y != defaultPos.y)
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(CloseDoor());
        }
    }

    // Functions for opening and closing the door.
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
