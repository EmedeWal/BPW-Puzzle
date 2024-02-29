using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    #region Tutorial
    [SerializeField] private GameObject[] tutorials;

    private int tCounter = 0;
    private bool tutorialOpen;
    #endregion

    [Header("References")]
    [SerializeField] private GameObject stickInfo;
    [SerializeField] private GameObject tutorialStick;
    [SerializeField] private DoorSystem doorSystem;
    [SerializeField] private PlayerController player;

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

    private void OpenCurrentTutorial()
    {
        if (tCounter < tutorials.Length)
        {
            tutorialOpen = true;
            player.canMove = false;
            tutorials[tCounter].gameObject.SetActive(true);
        }
    }

    private void CloseCurrentTutorial()
    {
        tutorialOpen = false;
        player.canMove = true;
        tutorials[tCounter].gameObject.SetActive(false);
        tCounter++;
    }

    #region Unity Events
    public void TriedDoor()
    {
        // After the player has attempted to enter the door, spawn the tutorialStick
        OpenCurrentTutorial();

        tutorialStick.SetActive(true);
    }

    public void TutorialStick()
    {
        // Activate relevant game object after the player has approached the stick
        OpenCurrentTutorial();

        stickInfo.SetActive(true);
    }

    public void AfterDoor()
    {
        // Give the player a new stick and close the door behind him
        OpenCurrentTutorial();

        player.AddSticks(1);

        doorSystem.ForceCloseDoor();
    }

    #endregion
}
