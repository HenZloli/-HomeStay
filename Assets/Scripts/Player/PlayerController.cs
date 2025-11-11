using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("===== Movement Settings =====")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 9f;
    public float acceleration = 20f;

    [Header("===== Mouse Look =====")]
    public Camera playerCamera;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 85f;

    private Rigidbody rb;
    private float cameraPitch = 0f;
    private bool isSprinting;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        HandleSprintInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void HandleSprintInput()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

        if (playerCamera)
        {
            Vector3 camRotation = playerCamera.transform.localEulerAngles;
            camRotation.x = cameraPitch;
            playerCamera.transform.localEulerAngles = camRotation;
        }
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(inputX, 0f, inputZ).normalized;

        if(input.magnitude == 0) return;

        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
        Vector3 targetVelocity = transform.TransformDirection(input) * targetSpeed;

        Vector3 newPosition = rb.position + targetVelocity * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

}
