using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.ShaderKeywordFilter;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;

    [HideInInspector] public bool canMove = true;

    private CharacterController characterController;

    private Vector3 moveDir;
    #endregion

    #region Camera Variables
    [Header("Camera Variables")]
    [SerializeField] Camera playerCamera;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float lookXLimit;

    private float rotationX = 0f;
    #endregion

    #region Stick Variables
    [Header("Stick References")]
    [SerializeField] private KeyCode interaction;
    [SerializeField] private GameObject stickGFX;
    [SerializeField] private GameObject stickPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TextMeshProUGUI stickTextUI;
    [SerializeField] private int sticksInInventory = 0;

    [HideInInspector] public GameObject stickToDestroy;
    [HideInInspector] public bool inRangeOfStick;

    public GameObject prompt;
    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        prompt.SetActive(false);

        UpdateUI();
    }

    private void Update()
    {
        if (canMove)
        {
            Movement();
            RotateCamera();

            StickInput();
        }
    }

    private void Movement()
    {
        Vector3 forward = transform.forward * Input.GetAxis("Vertical");
        Vector3 right = transform.right * Input.GetAxis("Horizontal");

        moveDir = (forward + right);

        moveDir *= moveSpeed;

        characterController.Move(moveDir * Time.deltaTime);
    }

    private void RotateCamera()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    private void StickInput()
    {
        if (!Input.GetKeyDown(interaction))
        {
            return;
        }

        if (inRangeOfStick)
        {
            inRangeOfStick = false;
            RetrieveStick();
        }
        else if (sticksInInventory >= 1)
        {
            DropStick();
        }
    }

    private void RetrieveStick()
    {
        // The player gains a stick and removes it from the world.
        sticksInInventory++;
        Destroy(stickToDestroy);

        // If the stickGFX is not active, set it active.
        if (!stickGFX.activeSelf) stickGFX.SetActive(true);
        if (prompt.activeSelf) prompt.SetActive(false);
        
        UpdateUI();
    }

    private void DropStick()
    {
        // The player loses a stick and spawns it in the world.
        sticksInInventory--;
        Instantiate(stickPrefab, spawnPoint.position, spawnPoint.rotation);

        // If the player has no sticks left, disable stickGFX.
        if (sticksInInventory == 0) stickGFX.SetActive(false);

        UpdateUI();
    }

    private void UpdateUI()
    {
        stickTextUI.text = "Sticks: " + sticksInInventory;
    }

    public void AddSticks(int amount)
    {
        sticksInInventory += amount;

        UpdateUI();
    }
}