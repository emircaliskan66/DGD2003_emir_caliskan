using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float _mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 5f) * 100f;

        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity  * Time.deltaTime;

        
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        
        playerBody.Rotate(Vector3.up * mouseX);
    }
}