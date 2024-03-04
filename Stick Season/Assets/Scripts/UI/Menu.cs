using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [HideInInspector] public bool menuOpen;

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        menuOpen = false;
        gameObject.SetActive(false);

        playerController.canMove = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Level 01");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
