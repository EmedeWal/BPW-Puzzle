using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 moveDir;

    [SerializeField] private float moveSpeed;

    private float xMove;
    private float zMove;
        
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void MyInput()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");
    }

    private void Movement()
    {
        moveDir = new Vector3(xMove, 0f, zMove);

        moveDir.Normalize();

        rb.velocity = moveDir * moveSpeed;
    }
}
