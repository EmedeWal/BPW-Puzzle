using UnityEngine;
using UnityEngine.Events;

public class TutorialStick : MonoBehaviour
{
    public UnityEvent tutorialStick;

    private bool tutorialSpawned;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !tutorialSpawned)
        {
            tutorialStick?.Invoke();
            tutorialSpawned = true;
        }
    }
}
