using UnityEngine;
using System.Collections;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private PressurePlate[] pressurePlates;

    // Variables to track whether the door should open or not.
    [HideInInspector] public int counter = 0;

    // Variables for door movement.
    private float offset = 10f;
    private float moveDelay = 0.01f;
    private float moveAmount = 0.1f;

    private Coroutine currentCoroutine;
    private string currentCoroutineName;

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

    private void Update()
    {
        foreach (PressurePlate plate in pressurePlates) 
        { 
            if (plate.isTriggered)
            {
                // For every pressurePlate that isTriggered, increase the counter.
                counter++;
            }
        }

        Manager();
    }

    private void Manager()
    {
        // If the counter is equal to the amount of plates and the coroutine has not been started yet, open the door.
        if (counter == pressurePlates.Length && currentCoroutineName != "Open Door")
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(OpenDoor());
        }
        else if (counter != pressurePlates.Length && currentCoroutineName != "Close Door")
        {
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(CloseDoor());
        }

        counter = 0;
    }

    // Functions for opening and closing the door.
    private IEnumerator OpenDoor()
    {
        currentCoroutineName = "Open Door";

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
        currentCoroutineName = "Close Door";

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
