using System.Collections;
using UnityEngine;

public class TrapSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PressurePlate plate;

    [Header("General")]
    [SerializeField] private LayerMask canKill;
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private float resetDuration;
    [SerializeField] private float offset;

    [Header("Descent")]
    [SerializeField] private float moveAmountD;
    [SerializeField] private float moveDelayD;

    [Header("Ascent")]
    [SerializeField] private float moveAmountA;
    [SerializeField] private float moveDelayA;

    #region Positions
    private Vector3 defaultPos;
    private Vector3 newPos;
    #endregion

    #region Tracking Bools
    private bool active;
    private bool lethal;
    #endregion

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
        if (plate.isTriggered && !active)
        {
            Debug.Log("Active plate detected");

            // If the pressure plate is triggered, lower the trap
            active = true;
            lethal = true;

            StartCoroutine(Descend());
        }

        if (lethal)
        {
            // Collect layers of all colliders detected within the box
            Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2f, Quaternion.identity);

            // Display the layers of all detected colliders
            foreach (Collider collider in colliders)
            {
                if ((canKill & (1 << collider.gameObject.layer)) != 0)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }

    // Functions for opening and closing the door.
    private IEnumerator Descend()
    {

        // While the gameObject is lower than the desired position, move it down
        while (transform.position.y > newPos.y)
        {
            // Move the trap down by the increment of moveAmount.
            Vector3 newPosition = transform.position + Vector3.down * moveAmountD;
            transform.position = newPosition;

            // Wait seconds equal to the moveDelay. Then, repeat.
            yield return new WaitForSeconds(moveDelayD);
        }

        lethal = false;

        // When the trap is at its lowest position, wait to reset
        yield return new WaitForSeconds(resetDuration);

        // Reset the position back to default
        StartCoroutine(Ascend());
    }

    private IEnumerator Ascend()
    {
        Debug.Log("Reset the position");

        // While the gameObject is higher than the desired position, move it up
        while (transform.position.y < defaultPos.y)
        {
            // Move the trap back to its original position by the increment of moveAmount.
            Vector3 newPosition = transform.position + Vector3.up * moveAmountA;
            transform.position = newPosition;

            // Wait seconds equal to the moveDelay. Then, repeat.
            yield return new WaitForSeconds(moveDelayA);
        }

        // Wait the reset duration. Then, reset "active"
        yield return new WaitForSeconds(resetDuration);

        active = false;
    }
}
