using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Tutorial
    [SerializeField] private GameObject[] tutorials;

    private int tCounter = 0;
    private bool tutorialOpen;
    #endregion

    #region Doors
    [SerializeField] private DoorSystem[] doors;

    private int doorCounter = 0;
    #endregion

    #region Spawn Points
    [SerializeField] private Transform[] spawnPoints;

    private static int spawnPointCounter;
    #endregion

    #region References
    [Header("References")]
    [SerializeField] private GameObject tutorialStick;
    [SerializeField] private GameObject stickInfo;
    [SerializeField] private GameObject player;

    private PlayerController playerController;
    #endregion

    private void Awake()
    {
        foreach (GameObject tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }

        tutorialStick.SetActive(false);

        // Get the current spawn point based on the spawnPointCounter
        Transform currentSpawnPoint = spawnPoints[spawnPointCounter];

        Debug.Log("Player spawned at Spawn Point " + spawnPointCounter);

        // Set the player's position and rotation to the current spawn point
        player.transform.position = currentSpawnPoint.position;
        player.transform.rotation = currentSpawnPoint.rotation;

        playerController = player.GetComponent<PlayerController>();
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
            playerController.canMove = false;
            tutorials[tCounter].gameObject.SetActive(true);
        }
    }

    private void CloseCurrentTutorial()
    {
        tutorialOpen = false;
        playerController.canMove = true;
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

        PlayerController.stickInfoActive = true;

        stickInfo.SetActive(true);
    }

    public void CompletedLevel0()
    {
        // Give the player two new sticks and close the door behind him
        OpenCurrentTutorial();

        playerController.AddSticks(2);

        DoorSystem currentDoor = doors[doorCounter];
        currentDoor.ForceCloseDoor();
        doorCounter++;

        spawnPointCounter++;


    }

    public void CompletedLevel1()
    {
        OpenCurrentTutorial();

        int sticks = 1;

        // Make sure the player has only two sticks for the next level.
        if (playerController.sticksInInventory == 1)
        {
            sticks--;
        }

        playerController.AddSticks(sticks);

        DoorSystem currentDoor = doors[doorCounter];
        currentDoor.ForceCloseDoor();
        doorCounter++;

        spawnPointCounter++;
    }

    public void CompletedLevel2()
    {
        spawnPointCounter = 0;

        SceneManager.LoadScene("End Screen");
    }
    #endregion
}
