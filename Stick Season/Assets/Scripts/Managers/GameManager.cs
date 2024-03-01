using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Tutorial
    [SerializeField] private GameObject[] tutorials;

    private int tCounter = 0;
    private bool tutorialOpen;
    #endregion

    [SerializeField] private DoorSystem[] doors;
    private int doorCounter = 0;

    [Header("References")]
    [SerializeField] private GameObject stickInfo;
    [SerializeField] private GameObject tutorialStick;
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
        if (tutorialOpen && Input.GetKeyDown(KeyCode.Space))
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

    public void CompletedLevel0()
    {
        // Give the player two new sticks and close the door behind him
        OpenCurrentTutorial();

        player.AddSticks(2);

        DoorSystem currentDoor = doors[doorCounter];
        currentDoor.ForceCloseDoor();
        doorCounter++;
    }

    public void CompletedLevel1()
    {
        OpenCurrentTutorial();

        int sticks = 1;

        // Make sure the player has only two sticks for the next level.
        if (player.sticksInInventory == 1)
        {
            sticks--;
        }

        player.AddSticks(sticks);

        DoorSystem currentDoor = doors[doorCounter];
        currentDoor.ForceCloseDoor();
        doorCounter++;
    }

    #endregion
}
