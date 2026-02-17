using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float speed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;

    [Header("Referanslar")]
    public Camera playerCamera;

    private Rigidbody rb;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontal).normalized;

        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void LateUpdate()
    {
        playerCamera.transform.position = transform.position + new Vector3(0f, 0.8f, 0f);
    }
}