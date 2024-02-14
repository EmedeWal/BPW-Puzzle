using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;

    [Header("Camera Variables")]
    [SerializeField] private float lookSpeed;
    [SerializeField] private float lookXLimit;

    [HideInInspector] public bool canMove = true;

    private float rotationX = 0f;

    private Vector3 moveDir;
    private float xMove;
    private float zMove;

    private CharacterController characterController;
    private Rigidbody rb;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {
        MyInput();
        Movement();
        RotateCamera();
    }

    private void MyInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
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
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}