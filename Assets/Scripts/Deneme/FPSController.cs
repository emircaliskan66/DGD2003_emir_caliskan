using UnityEngine;
using UnityEngine.UI; // UI bileþenleri (Image) için gerekli kütüphane

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    private float currentSpeed;

    [Header("Jump Settings")]
    public float jumpHeight = 1.5f;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;
    public float currentStamina;
    private bool isSprinting;

    [Header("UI References")]
    public Image staminaBarFill; // Ekranda dolup boþalacak olan sarý/yeþil bar

    [Header("Headbob Settings")]
    public Transform playerCamera;
    public float walkBobSpeed = 12f;
    public float walkBobAmount = 0.05f;
    public float sprintBobSpeed = 18f;
    public float sprintBobAmount = 0.12f;
    private float defaultCameraY;
    private float timer;

    [Header("Physics & Gravity")]
    public float gravity = -9.81f;
    private Vector3 velocity;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        if (playerCamera != null)
        {
            defaultCameraY = playerCamera.localPosition.y;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovementAndStamina();
        ApplyGravityAndJump();
        HandleHeadbob();
    }

    void HandleMovementAndStamina()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && move.magnitude > 0)
        {
            isSprinting = true;
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            isSprinting = false;
            currentSpeed = walkSpeed;

            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // UI BAR GÜNCELLEMESÝ (YENÝ EKLENEN KISIM)
        if (staminaBarFill != null)
        {
            // Fill Amount 0 ile 1 arasýnda bir deðer kabul ettiði için bölme iþlemi yapýyoruz
            staminaBarFill.fillAmount = currentStamina / maxStamina;
        }

        characterController.Move(move * currentSpeed * Time.deltaTime);
    }

    void ApplyGravityAndJump()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleHeadbob()
    {
        if (playerCamera == null) return;

        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;

        if (characterController.isGrounded && isMoving)
        {
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);
            float newY = defaultCameraY + Mathf.Sin(timer) * (isSprinting ? sprintBobAmount : walkBobAmount);
            playerCamera.localPosition = new Vector3(playerCamera.localPosition.x, newY, playerCamera.localPosition.z);
        }
        else
        {
            playerCamera.localPosition = new Vector3(
                playerCamera.localPosition.x,
                Mathf.Lerp(playerCamera.localPosition.y, defaultCameraY, Time.deltaTime * 10f),
                playerCamera.localPosition.z
            );
        }
    }
}