using UnityEngine;
using UnityEngine.Events;

public class TutorialStick : MonoBehaviour
{
    public UnityEvent tutorialStick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialStick?.Invoke();
        }
    }
}
