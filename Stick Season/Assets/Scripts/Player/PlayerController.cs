using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;

    private CharacterController characterController;

    private Vector3 moveDir;

    //[HideInInspector] public bool canMove = true;
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

    [HideInInspector] public bool inRangeOfStick;

    private bool holdingStick = true;

    #endregion

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {
        Movement();
        RotateCamera();

        StickInput();
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

        if (holdingStick)
        {
            DropStick();
        }
        else if (inRangeOfStick)
        {
            RetrieveStick();
        }

    }

    private void DropStick()
    {
        Debug.Log("The stick was dropped");

        holdingStick = false;
        stickGFX.SetActive(false);
        Instantiate(stickPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void RetrieveStick()
    {
        Debug.Log("The stick was retrieved");

        holdingStick = true;
        stickGFX.SetActive(true);
    }
}