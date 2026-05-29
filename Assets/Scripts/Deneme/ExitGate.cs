using System.Collections;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    [Header("Door Open Settings")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openDuration = 1.2f;

    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera doorCamera;
    [SerializeField] private float doorCameraViewTime = 1.2f;

    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (isOpen) return;

        Debug.Log("Sinyal alindi! Kapi aciliyor!");
        StartCoroutine(OpenDoorSequence());
    }

    private IEnumerator OpenDoorSequence()
    {
        isOpen = true;

        // Player camera kapanır, door camera açılır
        if (playerCamera != null)
            playerCamera.enabled = false;

        if (doorCamera != null)
            doorCamera.enabled = true;

        // Kapı açılır
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, openAngle, 0f);

        float timer = 0f;

        while (timer < openDuration)
        {
            timer += Time.deltaTime;

            float t = timer / openDuration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

        transform.rotation = targetRotation;

        // Kapıyı kısa süre göster
        yield return new WaitForSeconds(doorCameraViewTime);

        // Door camera kapanır, player camera geri açılır
        if (doorCamera != null)
            doorCamera.enabled = false;

        if (playerCamera != null)
            playerCamera.enabled = true;
    }
}