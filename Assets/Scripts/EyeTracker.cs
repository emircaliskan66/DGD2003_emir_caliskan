using UnityEngine;

public class EyeTracker : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 3f;

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}