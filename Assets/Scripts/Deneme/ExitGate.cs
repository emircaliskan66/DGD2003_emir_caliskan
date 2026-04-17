using UnityEngine;

public class ExitGate : MonoBehaviour
{

    public void OpenDoor()
    {
        Debug.Log("Sinyal alindi! Kapi aciliyor!");

        gameObject.SetActive(false);
    }
}