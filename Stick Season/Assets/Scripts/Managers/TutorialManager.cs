using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorials;
    [SerializeField] private GameObject stickInfo;
    [SerializeField] private GameObject tutorialStick;
    [SerializeField] private PlayerController controller;

    //[SerializeField] private GameObject stick;

    private int tCounter = 0;
    private bool tutorialOpen;

    private void Awake()
    {
        foreach (GameObject tutorial in tutorials) 
        {
            tutorial.SetActive(false);
        }

        stickInfo.SetActive(false);
        tutorialStick.SetActive(false);
    }

    private void Update()
    {
        if (tutorialOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCurrentTutorial();
        }
    }

    public void TriedDoor()
    {
        OpenCurrentTutorial();

        tutorialStick.SetActive(true);
    }

    public void TutorialStick()
    {
        OpenCurrentTutorial();

        stickInfo.SetActive(true);
    }

    private void OpenCurrentTutorial()
    {
        if (tCounter < tutorials.Length)
        {
            tutorialOpen = true;
            controller.canMove = false;
            tutorials[tCounter].gameObject.SetActive(true);
        }
    }

    private void CloseCurrentTutorial()
    {
        tutorialOpen = false;
        controller.canMove = true;
        tutorials[tCounter].gameObject.SetActive(false);
        tCounter++;
    }
}
