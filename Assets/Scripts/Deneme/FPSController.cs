using UnityEngine;
using UnityEngine.UI;

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

    // YENÝ: Bekleme Süresi (Cooldown) Deðiþkenleri
    public float regenCooldown = 1.5f; // Koþmayý býraktýktan sonra kaç saniye bekleyecek?
    private float currentCooldownTimer = 0f; // Arka planda sayan gizli kronometre

    public float currentStamina;
    private bool isSprinting;

    [Header("UI References")]
    public Image staminaBarFill;

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

        // EÐER KOÞUYORSAK
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && move.magnitude > 0)
        {
            isSprinting = true;
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDrainRate * Time.deltaTime;

            // YENÝ: Oyuncu koþtuðu sürece bekleme sayacýný sürekli baþa sar (örneðin 1.5 saniyeye)
            currentCooldownTimer = regenCooldown;
        }
        // EÐER KOÞMUYORSAK (Yürüyor veya Duruyorsa)
        else
        {
            isSprinting = false;
            currentSpeed = walkSpeed;

            // YENÝ: Önce bekleme sayacýný geriye doðru saydýr
            if (currentCooldownTimer > 0)
            {
                currentCooldownTimer -= Time.deltaTime;
            }
            // Sayaç 0'ý vurduysa, staminayý doldurmaya baþla
            else if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (staminaBarFill != null)
        {
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

    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }
}