using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        // Fare imlecini ekranýn ortasýna kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Farenin X ve Y eksenindeki hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukarý/Aþaðý bakma (X ekseninde rotasyon)
        xRotation -= mouseY;
        // Kameranýn 90 dereceden fazla yukarý veya aþaðý dönmesini engelle (ters dönmemek için)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kamerayý yukarý/aþaðý döndür
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakterin tüm vücudunu saða/sola döndür
        playerBody.Rotate(Vector3.up * mouseX);
    }
}